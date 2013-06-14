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

	$(".contAlert .delete").click(function () {
		$(this).parents(".contAlert").animate({ opacity: 'hide' }, "slow");
	});

	$('.Count').click(function () {
		bId = $(this).attr("id")
		$.post("/Books/UpCount", { id: bId }, function (data) { });
	});

});

function readedAlert(aId, iId) {
	$.post("/Home/ReadedAlertAjax", { AlertId: aId, IBONum: iId, datetime : null }, function (data) { });
	CountAlert -= 1;
	$("#Cont").html(CountAlert);
}