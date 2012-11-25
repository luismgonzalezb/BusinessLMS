
$(document).ready(function () {
    $(".feed-link-action").click(function () {
        openTicketWindow();
        return false;
    });
});

function openTicketWindow() {
    $("#ticketModal").load('/Tickets/CreateTicket' , function () {
        $("#ticketModal").modal({
            closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
            position: ["20%", ],
            overlayId: 'modalWindow-overlay',
            containerId: 'modalWindow-content',
            onOpen: function (dialog) {
                dialog.overlay.fadeIn('slow', function () {
                    dialog.data.hide();dialog.container.fadeIn('slow', function () {dialog.data.slideDown('fast');});
                });
            },
            onClose: function (dialog) {
                dialog.data.fadeOut('slow', function () {
                    $.modal.close();
                    $("#ticketModal").hide();
                });
            }
        });
    });
}

function submitTicketForm(frm) {
    if (!$(frm).valid()) {
        return false;
    }
    $.post($(frm).attr("action"), $(frm).serialize(), function (data) {
        if (data.success == true) {
            $.modal.close();
        }
    });
    return false;
}