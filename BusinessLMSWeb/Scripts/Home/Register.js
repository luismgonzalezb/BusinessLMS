﻿$(function () {
    $.validator.unobtrusive.parse("#current_form");
    $("#iboName").autocomplete({
        source: "/Home/SearchIBO/",
        select: function (event, ui) {
            $("#iboName").val(ui.item.label);
            $("#divIboNum").html(ui.item.value);
            $.get("/Home/GetIBO/" + ui.item.value, function (data) {
                $("#divIboEmail").html(data.email);
                $("#divIboPhone").html(data.phone);
                $("#IBONum").val(ui.item.value);
            });
            return false;
        }
    });
});


function sendRegister(frm) {

    if (!$(frm).valid()) { return false; }
    $.post($(frm).attr("action"), $(frm).serialize(), function (data) {
        if (data.success == true) {
            $("#divRegisterForm").html("<h2>Thanks For Registering... </h2>");
        }
    });
}