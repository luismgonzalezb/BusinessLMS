function editTicket(id) {
    $("#ticketInfo").hide("slow");
    $("#ticketInfo").load('/Tickets/EditTicket/' + id).show("slow");
}

function cancelticket() {
    $("#ticketInfo").hide("slow");
}

function updatetickets(frm) {
    if (!$(frm).valid()) { return false; }
    $.post($(frm).attr("action"), $(frm).serialize(), function (data) {
        if (data.success == true) {
            document.location.reload(true);
        } else {
            $("#ticketInfo").hide("slow");
        }
    });
}