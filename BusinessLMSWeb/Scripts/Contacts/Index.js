var deleteLinkObj;

$(document).ready(function () {

	$('#contacts-table').dataTable({
		"bJQueryUI": true,
		"sPaginationType": "full_numbers"
	});

	$("#contactInfo").hide();

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
					}
				});
				$(this).dialog("close");
			},
			"Cancel": function () {
				$(this).dialog("close");
			}
		}
	});

});

function AddNewContact() {
	$("#contactInfo").hide("slow").load('/Contacts/NewContact').show("slow");
}

function editContact(id) {
	$("#contactInfo").hide("slow").load('/Contacts/EditContact/' + id).show("slow");
}

function CancelContact() {
	$("#contactInfo").hide("slow");
}

function submitform(frm) {
	if (!$(frm).valid()) { return false; }
	console.log($(frm).serialize());
	$.post($(frm).attr("action"), $(frm).serialize(), function (data) {
		console.log(data);
		if (data.contactId != null) {
			$("#contactInfo").hide("slow", function () {
				document.location.reload(true);
			});
		} else {
			var noty_err = noty({
				type: "error",
				layout: 'bottom',
				text: data.message,
				timeout: 1000
			});
		}
	});
	return false;
}

function submitupdateform(frm) {
	if (!$(frm).valid()) { return false; }
	$.post($(frm).attr("action"), $(frm).serialize(), function (data) {
		if (data.success == true) {
			document.location.reload(true);
		} else {
			var noty_err = noty({
				type: "error",
				layout: 'bottom',
				text: data.message,
				timeout: 1000
			});
		}
	});
}
