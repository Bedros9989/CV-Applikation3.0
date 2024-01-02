document.addEventListener("DOMContentLoaded", function () {
    var colors = ["#65c8d0", "#ea4c89", "#ffcf06"];

    var links = document.querySelectorAll(".navbar-center li a");

    links.forEach(function (link) {
        link.addEventListener("mouseenter", function () {
            var randomIndex = Math.floor(Math.random() * colors.length);
            var randomColor = colors[randomIndex];
            link.style.setProperty("--after-background", randomColor);
        });

        link.addEventListener("mouseleave", function () {
            link.style.setProperty("--after-background", ""); 
        });
    });
});