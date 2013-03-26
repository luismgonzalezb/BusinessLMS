var deleteLinkObj;

$(document).ready(function () {
    $('#data-table').dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers"
    });

    $("#followupInfo").hide();

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
});

function AddNewItem() {
    $("#followupInfo").hide("slow").load('/Followups/NewFollowup').show("slow");
}

function EditItem(id) {
    $("#followupInfo").hide("slow").load('/Followups/EditFollowup/' + id).show("slow");
}

function CancelItem() {
    $("#followupInfo").hide("slow");
}

function submitform(frm) {
    if (!$(frm).valid()) { return false; }
    $.post($(frm).attr("action"), $(frm).serialize(), function (data) {
    	if (data.success == true) {
    		$("#followupInfo").hide("slow", function () {
    			location.reload(false);
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
            $("#followupInfo").hide("slow");
        }
    });
}

function actionsColumn(id) {
    result = ' <a class="edit-link-sm-black" href="/Contacts/Edit/' + id + '" title="Edit Contact">Edit</a> ';
    result += ' <a class="del-link-sm-black deletelink" href="/Contacts/DeleteContactAjax/' + id + '" title="Delete Contact">Delete</a> ';
    return result;
}
