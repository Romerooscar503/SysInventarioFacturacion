$(document).ready(function () {
    var $menuToggle = $('.menu-toggle');
    var $dashboardNav = $('.dashboard-nav-list');

    $menuToggle.on('click', function () {
        $dashboardNav.toggleClass('menu-hidden');
    });
});

window.addEventListener('scroll', function () {
    const header = document.querySelector('.fixed-header');
    if (window.scrollY > 0) {
        header.classList.add('scrolled');
    } else {
        header.classList.remove('scrolled');
    }
});

$(document).ready
    (
        function () {
            $('.labelMenu').click
                (
                    function () {
                        $('.menu').css('left', 0);
                        $('.opacMenu').fadeIn();
                    }
                )

            $('.opacMenu').click
                (
                    function () {
                        $('.menu').css('left', '-250px');
                        $('.opacMenu').fadeOut();
                    }
                )
        }
    )

