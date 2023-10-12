//Funcion de agregar al carrito y calcular
document.addEventListener("DOMContentLoaded", function () {
    const agregarProductoButtons = document.querySelectorAll(".agregar-producto");
    const eliminarProductoButtons = document.querySelectorAll(".eliminar-producto");
    const txtsubtotal = document.getElementById("txtsubtotal");
    const txtigv = document.getElementById("txtigv");
    const txttotal = document.getElementById("txttotal");
    const txtmontopago = document.getElementById("txtmontopago");
    const btncalcular = document.getElementById("btncalcular");

    const detalleFacturas = [];

    agregarProductoButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            const codigo = button.getAttribute("data-codigo");
            const nombre = button.getAttribute("data-nombre");
            const descripcion = button.getAttribute("data-descripcion");
            const precio = parseFloat(button.getAttribute("data-precio"));

            const cantidad = prompt(`Ingrese la cantidad para "${nombre}"`);
            if (cantidad !== null && !isNaN(cantidad) && cantidad > 0) {
                const importeTotal = parseFloat(cantidad) * precio;
                detalleFacturas.push({
                    codigo: codigo,
                    nombre: nombre,
                    descripcion: descripcion,
                    cantidad: parseFloat(cantidad),
                    precio: precio,
                    importeTotal: importeTotal,
                });

                updateDetalleFacturaTable();
                calculateTotal();
            }
        });
    });

    eliminarProductoButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            const id = parseInt(button.getAttribute("data-id"));
            const index = detalleFacturas.findIndex((detalle) => detalle.id === id);
            if (index !== -1) {
                detalleFacturas.splice(index, 1);
                updateDetalleFacturaTable();
                calculateTotal();
            }
        });
    });

    function updateDetalleFacturaTable() {
        const tbody = document.querySelector("#tablaDetalleFactura tbody");
        while (tbody.firstChild) {
            tbody.removeChild(tbody.firstChild);
        }

        detalleFacturas.forEach(function (detalle) {
            const row = document.createElement("tr");
            row.innerHTML = `
                        <td>${detalle.cantidad}</td>
                        <td>${detalle.nombre}</td>
                        <td>${detalle.descripcion}</td>
                        <td>${detalle.precio}</td>
                        <td>${detalle.importeTotal}</td>
                        <td>
                            <button class="eliminar-producto btn btn-danger" data-id="${detalle.id}">
                                <i class="fas fa-trash"></i>
                            </button>
                        </td>
                    `;
            tbody.appendChild(row);
        });
    }

    function calculateTotal() {
        let subtotal = 0;
        detalleFacturas.forEach(function (detalle) {
            subtotal += detalle.importeTotal;
        });

        const igv = subtotal * 0.18; // IGV del 18%
        const total = subtotal + igv;

        txtsubtotal.value = subtotal.toFixed(2);
        txtigv.value = igv.toFixed(2);
        txttotal.value = total.toFixed(2);
    }

    btncalcular.addEventListener("click", function () {
        const montoPago = parseFloat(txtmontopago.value);
        if (isNaN(montoPago) || montoPago < 0) {
            alert("Ingrese un monto de pago válido.");
            return;
        }

        const total = parseFloat(txttotal.value);
        if (montoPago < total) {
            alert("El monto de pago debe ser igual o mayor que el total.");
        } else {
            const cambio = montoPago - total;
            alert(`Cambio: S/. ${cambio.toFixed(2)}`);
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const txtCodigo = document.getElementById("txtCodigo");
    const txtNombre = document.getElementById("txtNombre");
    const tablaBusqueda = document.querySelector("#tablaBusqueda tbody");

    txtCodigo.addEventListener("input", filterProductos);
    txtNombre.addEventListener("input", filterProductos);

    function filterProductos() {
        const codigo = txtCodigo.value.trim().toLowerCase();
        const nombre = txtNombre.value.trim().toLowerCase();

        const rows = tablaBusqueda.querySelectorAll("tr");
        rows.forEach((row) => {
            const codigoCell = row.querySelector("td:nth-child(1)");
            const nombreCell = row.querySelector("td:nth-child(2)");
            if (codigoCell && nombreCell) {
                const codigoText = codigoCell.textContent.toLowerCase();
                const nombreText = nombreCell.textContent.toLowerCase();

                if (codigoText.includes(codigo) && nombreText.includes(nombre)) {
                    row.style.display = "";
                } else {
                    row.style.display = "none";
                }
            }
        });
    }
});

//funcion de filtar


$(document).ready(function () {
    // Maneja el evento de entrada de texto
    $("#inputProducto").keyup(function () {
        var textoBusqueda = $(this).val().toLowerCase();

        // Filtra los productos que coinciden con el texto de búsqueda
        var productosFiltrados = productos.filter(function (producto) {

            return producto.toLowerCase().includes(textoBusqueda);
        });

        // Actualiza la lista desplegable con los resultados de la búsqueda
        var selectProducto = $("#selectProducto");
        selectProducto.empty();
        console.log("filtrando...");
        // Agrega las opciones de productos encontrados
        productosFiltrados.forEach(function (producto) {
            selectProducto.append(new Option(producto, producto));
        });

        // Muestra u oculta la lista desplegable según si hay resultados
        if (productosFiltrados.length > 0) {
            selectProducto.show();
        } else {
            selectProducto.hide();
        }
        console.log(productosFiltrados);
    });

    // Maneja el evento de selección en la lista desplegable
    $("#selectProducto").change(function () {
        var seleccionado = $(this).val();
        // Puedes hacer algo con el producto seleccionado, como asignarlo a un campo oculto o procesarlo de alguna manera.
        console.log("Producto seleccionado:", seleccionado);
    });
});

$(document).ready(function () {
    // Manejar el clic en el botón de "Eliminar"
    $('#tablaDetalleFactura').on('click', '.eliminar-producto', function () {
        var row = $(this).closest('tr');
        row.remove(); // Remover la fila visualmente
    });
});

