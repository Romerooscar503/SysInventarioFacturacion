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
const darkModeToggle = document.getElementById("dark-mode-toggle");
const body = document.body;

darkModeToggle.addEventListener("click", () => {
    // Alterna la clase dark-mode en el cuerpo
    body.classList.toggle("dark-mode");
});

