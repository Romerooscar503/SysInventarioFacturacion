const delay = 4000; // ms

document.querySelectorAll(".carousel").forEach((carousel) => {
    const slides = carousel.querySelector(".slides");
    const slidesCount = slides.childElementCount;
    const maxLeft = (slidesCount - 1) * 100 * -1;
    let current = 0;

    function changeSlide(next = true) {
        if (next) {
            current += current > maxLeft ? -100 : current * -1;
        } else {
            current = current < 0 ? current + 100 : maxLeft;
        }

        slides.style.left = current + "%";
    }

    let autoChange = setInterval(changeSlide, delay);

    const restart = function () {
        clearInterval(autoChange);
        autoChange = setInterval(changeSlide, delay);
    };

    // Controles para el carrusel actual
    carousel.querySelector(".next-slide").addEventListener("click", function () {
        changeSlide();
        restart();
    });

    carousel.querySelector(".prev-slide").addEventListener("click", function () {
        changeSlide(false);
        restart();
    });
});

/*Modo Oscuro*/
const switchContainer = document.querySelector(".switch");
const slider = document.querySelector(".slider");
const body = document.body;
let isOn = false;

switchContainer.addEventListener("click", () => {
    isOn = !isOn;
    if (isOn) {
        slider.style.transition = "left 0.3s ease-in-out"; // Agrega transición
        slider.style.left = "60px"; // Desplaza hacia la derecha
        switchContainer.style.transition = "background 0.3s ease-in-out"; // Transición para el fondo
        switchContainer.style.background = "#007BFF";
        body.classList.add("dark-mode"); // Activa el modo oscuro
    } else {
        slider.style.transition = "left 0.3s ease-in-out"; // Agrega transición
        slider.style.left = "0"; // Desplaza hacia la izquierda
        switchContainer.style.transition = "background 0.3s ease-in-out"; // Transición para el fondo
        switchContainer.style.background = "#ccc";
        body.classList.remove("dark-mode"); // Desactiva el modo oscuro
    }
});


