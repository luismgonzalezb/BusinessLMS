var deleteLinkObj;

$(document).ready(function () {

    $('#books-table').dataTable({
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
        if (data.contactId != null) {
            $("#booksInfo").hide("slow");
            updateTable(data);
        } else {
            $("#booksInfo").hide("slow");
        }
    });
    return false;
}

function updateTable(data) {
    $('#books-table').dataTable().fnAddData([
		data.firstName,
		data.lastName,
		publicColumn(data.isPublic),
		actionsColumn(data.bookId)
    ]);
}

function publicColumn(isPublic) {
    checked = (isPublic == true) ? "checked" : "";
    result = ' <input class="check-box" type="checkbox" disabled="disabled" ' + checked + '> ';
    return result;
}

function actionsColumn(id) {
    result += ' <a class="del-link-sm-black deletelink" href="/Books/DeleteBookAjax/' + id + '" title="Delete Book">Delete</a> ';
    return result;
}

$(document).ready(function () {
    $('.Count').click(function () {
        bId = $(this).attr("id")
        $.post("/Books/UpCount", { id: bId }, function (data) {

        });

    });
});
