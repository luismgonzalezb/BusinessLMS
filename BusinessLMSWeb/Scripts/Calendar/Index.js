
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
			var title = prompt('Event Title:');
			if (title) {
				calendar.fullCalendar('renderEvent',
					{
						title: title,
						start: start,
						end: end,
						allDay: allDay
					},	
					true // make the event "stick"
				);
			}
			calendar.fullCalendar('unselect');
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
			console.log(event);
			if (event.source) {
				if (event.source.data.type == "4")
				if (event.url) {
					window.open(event.url);
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

function openCalendarModal(url)
{

}