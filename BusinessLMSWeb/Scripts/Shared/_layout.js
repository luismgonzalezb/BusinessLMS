
$(document).ready(function () {
    $.ajaxSetup({
        cache: false
    });
    $("div.profile").mouseover(function () {
        $(this).find('ul').css('visibility', 'visible');
    }).mouseout(function () {
        $(this).find('ul').css('visibility', 'hidden');
    });
});

$(document).ready(function(){
    $(".contAlert .delete").click(function () {
        $(this).parents(".contAlert").animate({ opacity: 'hide' }, "slow");

    });
});

function readedAlert(aId, iId) {
    $.post("/Home/ReadedAlertAjax", { alertId : aId, IBONum : iId }, function(data) {

    });
}