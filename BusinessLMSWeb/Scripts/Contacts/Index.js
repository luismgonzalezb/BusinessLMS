
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

});