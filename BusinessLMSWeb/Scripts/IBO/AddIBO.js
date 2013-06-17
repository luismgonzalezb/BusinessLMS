
$(document).ready(function () {

    $(".date-picker").datepicker({
        yearRange: "-90:+0",
        changeMonth: true,
        changeYear: true
    });

    $('#file_upload').ajaxForm({
        beforeSubmit: function (a, f, o) {
            o.dataType = 'json';
            $('#status').html('Submitting...');
        },
        success: function (data) {
            file = data[0]
            $("#picture").val(file.Location);
            var $out = $('#status');
            $out.html('<img src="' + file.Location + '" style="max-width: 250px;" />');
        }
    });
    
});

function submitform() {
    $("#createIBOForm").submit();
}

$(function () {
    $("#iboName").autocomplete({
        source: "/Home/SearchIBO/",
        search: function (event, ui) {
            $("#divLoading").showLoading();
        },
        response: function (event, ui) {
            $("#divLoading").hideLoading();
        },
        select: function (event, ui) {
            $("#iboName").val(ui.item.value);
            $("#divIboNum").html(ui.item.value);
            $("#UPLine").val(ui.item.value);
            return false;
        }
    });
});