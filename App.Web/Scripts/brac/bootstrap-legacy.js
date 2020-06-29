$(document).ready(function () {
    $(".toggle-trigger").on("click", function () {
        $(".dt-carret").toggleClass("active");
        $(".tnav").slideToggle();
    });
    $(".dropdown").click(function () {

        if ($(this).find(".dropdown-menu").hasClass("show") === true) {
            $(this).find(".dropdown-menu").removeClass("show");

        } else {
            $(this).find(".dropdown-menu").addClass("show");

        }
    });
    $(".long-nav-toggler").on("click", function () {
        $(".long-nav-items-wrapper").slideToggle();
    });
    $("a.sidebar-item").click(function (e) {
        e.stopPropagation();
    });
    $("div.sidebar-item").click(function () {
        var target = $(this).attr("data-target");
        $(target).slideToggle();
    })
    $(".mobile-drawer").on("click", function () {
        console.log("cliked");
        $(".sidebar").toggleClass("in");
    });


});