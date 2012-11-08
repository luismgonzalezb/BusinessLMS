var deleteLinkObj;

$(document).ready(function () {

    $('#contacts-table').dataTable({
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

    $("#contactInfo").hide();

    $(".help-link-action").click(function () {
        $("#info-modal").dialog("open");
        return false;
    });

    $('.deletelink').click(function () {
        deleteLinkObj = $(this);  //for future use
        $('#delete-dialog').dialog('open');
        return false; // prevents the default behaviour
    });

    $('#delete-dialog').dialog({
        autoOpen: false, width: 400, resizable: false, modal: true, //Dialog options
        buttons: {
            "Continue": function () {
                $.post(deleteLinkObj[0].href, function (data) {  //Post to action
                    if (data.success == true) {
                        deleteLinkObj.closest("tr").hide('fast'); //Hide Row
                        //(optional) Display Confirmation
                    }
                    else {
                        //(optional) Display Error
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
    $("#contactInfo").hide("slow");
    $("#contactInfo").load('/Contacts/NewContact').show("slow");
}

function CancelContact() {
    $("#contactInfo").hide("slow");
}

function submitform(frm) {
    if (!$(frm).valid()) { return false; }
    $.post($(frm).attr("action"), $(frm).serialize(), function (data) {
        if (data.contactId != null) {
            $("#contactInfo").hide("slow");
            $('#contacts-table').dataTable().fnAddData([
                data.firstName,
                data.lastName,
                data.email,
                data.phone,
                data.cell,
                data.preferred,
                data.contactLevel,
                publicColumn(data.isPublic),
                actionsColumn(data.contactId)
            ]);
        } else {
            $("#contactInfo").hide("slow");
        }
    });
    return false;
}

function publicColumn(isPublic) {
    checked = (isPublic == true) ? "checked" : "";
    result = ' <input class="check-box" type="checkbox" disabled="disabled" ' + checked + '> ';
    return result;
}

function actionsColumn(id) {
    result = ' <a class="edit-link-sm-black" href="/Contacts/Edit/' + id + '" title="Edit Contact">Edit</a> ';
    result += ' <a class="del-link-sm-black deletelink" href="/Contacts/DeleteContactAjax/' + id + '" title="Delete Contact">Delete</a> ';
    return result;
}
