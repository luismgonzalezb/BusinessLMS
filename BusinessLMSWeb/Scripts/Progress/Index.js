$(document).ready(function () {

	$('#goalprogress-table').dataTable({
		"bJQueryUI": true,
		"sPaginationType": "full_numbers"
	});

	$("#info-modal").dialog({
		autoOpen: false,
		show: "blind",
		height: 600,
		width: 800,
		modal: true
	});

	$(".help-link-action").click(function () {
		$("#info-modal").dialog("open");
		return false;
	});

	$("#progressInfo").hide();

});

function addProgress(id) {
	$("#progressInfo").hide("slow").load('/Progress/CreateProgress/' + id).show("slow");
}

function CancelProgress() {
	$("#progressInfo").hide("slow");
}

function submitform(frm) {
	if (!$(frm).valid()) { return false; }
	$.post($(frm).attr("action"), $(frm).serialize(), function (data) {
		if (data.contactId != null) {
			$("#progressInfo").hide("slow");
			updateTable(data);
		} else {
			$("#progressInfo").hide("slow");
		}
	});
	return false;
}