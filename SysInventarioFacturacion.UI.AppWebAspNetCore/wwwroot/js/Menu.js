$(document).ready(function () {
    var $menuToggle = $('.menu-toggle');
    var $dashboardNav = $('.dashboard-nav-list');

    $menuToggle.on('click', function () {
        $dashboardNav.toggleClass('menu-hidden');
    });
});