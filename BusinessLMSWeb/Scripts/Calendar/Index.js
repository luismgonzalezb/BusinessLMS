
$(document).ready(function () {
	/******************************************/
	var date = new Date();
	var d = date.getDate();
	var m = date.getMonth();
	var y = date.getFullYear();

	var calendar = $('#calendar').fullCalendar({
		theme: true,
		selectable: true,
		selectHelper: true,
		editable: true,
		header: {
			left: 'prev,next today',
			center: 'title',
			right: 'month,agendaWeek,agendaDay'
		},
		select: function (start, end, allDay) {
			var dateFormatter = moment(start);
			var url = '/Followups/NewFollowupDate?date=' + dateFormatter.format('L');
			console.log(url);
			openCalendarModal(url);
		},
		events: {
			url: "/Calendar/GetEvents/",
			type: "POST",
			data: {
				type: calendarType
			},
			error: function() {
				alert('there was an error while fetching events!');
			}
		},
		eventClick: function (event) {
			if (event.source) {
				if (event.source.data.type == "4") {
					if (event.url) {
						window.open(event.url);
						return false;
					}
				} else {
					openCalendarModal(event.url);
					return false;
				}
			}
		},
		loading: function (bool) {
			if (bool)
				$("#calendar").showLoading({ vPos: "top" });
			else
				$("#calendar").hideLoading();
		}
	});

});

function openCalendarModal(url, data)
{
	$("#modalWindow").load(url, function () {
		$("#modalWindow").modal({
			closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
			position: ["10px", ],
			overlayId: 'modalWindow-overlay',
			containerId: 'modalWindow-content',
			onOpen: function (dialog) {
				dialog.overlay.fadeIn('slow', function () {
					dialog.data.hide();
					dialog.container.fadeIn('slow', function () {
						dialog.data.slideDown('fast');
					});
				});
			},
			onClose: function (dialog) {
				dialog.data.fadeOut('slow', function () {
					$.modal.close();
					$("#modalWindow").hide();
				});
			}
		});
	});
}