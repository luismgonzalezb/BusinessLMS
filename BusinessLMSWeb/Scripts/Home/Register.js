$(function () {
    $("#iboName").autocomplete({
        source: "/Home/SearchIBO/",
        select: function (event, ui) {
            $("#iboName").val(ui.item.label);
            $("#iboNum").html(ui.item.value);
            $.get("/Home/GetIBO/" + ui.item.value, function (data) {
                $("#iboEmail").html(data.email);
                $("#iboPhone").html(data.phone);
            });
            return false;
        }
    });
});

