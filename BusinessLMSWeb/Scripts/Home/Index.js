$(document).ready(function () {
    $("a[data-gal^='prettyPhoto']").prettyPhoto({
        show_title:false,
        social_tools: false
    });
    $("#myController").jFlow({
        controller: ".jFlowControl",
        slideWrapper: "#jFlowSlider",
        slides: "#mySlides",
        selectedWrapper: "jFlowSelected",
        width: "960px",
        height: "350px",
        duration: 400,
        prev: ".jFlowPrev",
        next: ".jFlowNext"
    });
});

