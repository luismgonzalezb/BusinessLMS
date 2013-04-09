var deleteLinkObj;

$(document).ready(function () {

	$('#books-table').dataTable({
		"bJQueryUI": true,
		"sPaginationType": "full_numbers"
	});

	$("#booksInfo").hide();

	$('.deletelink').click(function () {
		deleteLinkObj = $(this);  //for future use
		$('#delete-dialog').dialog('open');
		return false;
	});

	$('#delete-dialog').dialog({
		autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
		buttons: {
			"Continue": function () {
				$.post(deleteLinkObj[0].href, function (data) {  //Post to action
					if (data.success == true) {
						deleteLinkObj.closest("tr").hide('fast'); //Hide Row
					} else { }
				});
				$(this).dialog("close");
			},
			"Cancel": function () {
				$(this).dialog("close");
			}
		}
	});

	$('.Count').click(function () {
		bId = $(this).attr("id");
		$.post("/Books/UpCount", { id: bId });
	});

});

function AddNewBook() {
	$("#booksInfo").hide("slow").load('/Books/CreateBook').show("slow");
}

function CancelBook() {
	$("#booksInfo").hide("slow");
}

function submitform(frm) {
	if (!$(frm).valid()) { return false; }
	$.post($(frm).attr("action"), $(frm).serialize(), function (data) {
		document.location.reload(true);
	});
	return false;
}

function activateSec() {
	$("#SecondLink").show("slow");
}

function activateThird() {
	$("#ThirdLink").show("slow");
}