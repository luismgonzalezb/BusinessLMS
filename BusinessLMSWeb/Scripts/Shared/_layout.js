var CountAlert;
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
	$.post("/Home/ReadedAlertAjax", { AlertId: aId, IBONum: iId, datetime : null }, function (data) {

	});
	CountAlert -= 1;
	$("#Cont").html(CountAlert);
	
}

$(document).ready(function () {
    $('.Count').click(function () {
        bId=$(this).attr("id")
        $.post("/Books/UpCount", { id:bId }, function (data) {
     
                });

    });
});
