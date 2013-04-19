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
	$(".help-link-action").click(function () {
		$("#info-containder").load('/' + CurretController + '/_HelpInfo', function () {
			$("#info-modal").dialog({
				autoOpen: false,
				show: "blind",
				height: 600,
				width: 800,
				modal: true
			});
			$("#info-modal").dialog("open");
		});
		return false;
	});
});

$(document).ready(function(){
	$(".contAlert .delete").click(function () {
		$(this).parents(".contAlert").animate({ opacity: 'hide' }, "slow");
	});
});

function readedAlert(aId, iId) {
	$.post("/Home/ReadedAlertAjax", { AlertId: aId, IBONum: iId, datetime : null }, function (data) { });
	CountAlert -= 1;
	$("#Cont").html(CountAlert);
}

$(document).ready(function () {
	$('.Count').click(function () {
		bId=$(this).attr("id")
		$.post("/Books/UpCount", { id:bId }, function (data) { });
	});
});


//FACEBOOK

$("#btnLogin").click(function (e) {
    e.preventDefault();
    FB.login(function (response) {
        if (response.authResponse) {
            $("#btnLogin").html("Logged in");
        } else {
            $("#btnLogin").html("Not logged in");
        }
    }, { scope: 'user_photos, friends_photos' });
});

$(".photoSelect").click(function (e) {
    e.preventDefault();
    id = null;
    if ($(this).attr('data-id')) id = $(this).attr('data-id');
    fbphotoSelect(id);
});