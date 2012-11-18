﻿var deleteLinkObj;

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

    $(".help-link-action").click(function () {
        $("#info-modal").dialog("open");
        return false;
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

function AddNewContact() {
    $("#contactInfo").hide("slow");
    $("#contactInfo").load('/Contacts/NewContact').show("slow");
}

function editContact(id) {
    $("#contactInfo").hide("slow");
    $("#contactInfo").load('/Contacts/EditContact/' + id).show("slow");
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

function submitupdateform(frm) {
    if (!$(frm).valid()) { return false; }
    $.post($(frm).attr("action"), $(frm).serialize(), function (data) {
        if (data.success == true) {
            document.location.reload(true);
        } else {
            $("#contactInfo").hide("slow");
        }
    });
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