
$(document).ready(function () {
    $("div.profile").mouseover(function () {
        $(this).find('ul').css('visibility', 'visible');
    }).mouseout(function () {
        $(this).find('ul').css('visibility', 'hidden');
    });
});
