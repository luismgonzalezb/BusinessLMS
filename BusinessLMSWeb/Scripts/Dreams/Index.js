
$(document).ready(function () {

    $("#info-modal").dialog({
        autoOpen: false,
        show: "blind",
        height: 600,
        width: 800,
        modal: true
    });

    $(".help-link").click(function () {
        $("#info-modal").dialog("open");
        return false;
    });

    $(".dreams").kwicks({
        min: 100,
        spacing: 0,
        sticky: true,
        event: 'click'
    });

    var currentForm;

    $("[id^=file_upload]").ajaxForm({
        beforeSubmit: function (a, f, o) {
            currentForm = f;
            o.dataType = 'json';
            var $out = $(currentForm).find("#status");
            if (!a[0].value) {
                $out.html('<div style="display:table-cell; vertical-align:middle">Please Select Picture</div>');
                return false;
            } else {
                $out.html('<div style="display:table-cell; vertical-align:middle">Submitting... <img src="/Images/loading.gif" /></div>');
            }
        },
        success: function (data) {
            file = data[0];
            var $out = $(currentForm).find("#status");
            $out.html('<img src="' + file.Location + '" style="max-width: 250px; max-height: 200px; " />');
            $(currentForm).closest(".metro-container-300").find("#picture").val(file.Location);
            $(currentForm).closest("ul").css("height", "550px");
            $(currentForm).closest("ul").find("li").animate({ height: 550 });
            
        }
    });

});

function submitform(btn) {
    $(btn).closest(".metro-container-300").find("[id^=createIBOForm]").submit();
}