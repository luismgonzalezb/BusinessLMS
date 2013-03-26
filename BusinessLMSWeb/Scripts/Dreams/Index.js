var currentForm;
var edit;

$(document).ready(function () {

	$(".date-picker").datepicker({
		yearRange: "-0:+30",
		changeMonth: true,
		changeYear: true
	});

	$(".dreams").kwicks({
		min: 100,
		spacing: 0,
		sticky: true,
		event: 'click'
	});

	setFileUpload();

	// DO NOT DELETE
	//$(".set-admin-user-link").tipTip({ maxWidth: "auto", defaultPosition: "right", delay: 500, edgeOffset: 5 });

});

function setFileUpload() {
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
			if (edit) {
				$("#modalWindow-content").animate({ height: 570 });
			} else {
				$(currentForm).closest("ul").css("height", "570px");
				$(currentForm).closest("ul").find("li").animate({ height: 570 });
			}
		}
	});
}

function submitform(btn) {
	$(btn).closest(".metro-container-300").find("[id^=createIBOForm]").submit();
}

function openEditWindow(id) {
	$("#modalWindow").load('/Dreams/EditDream/' + id, function () {
		edit = true;
		setFileUpload();
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
						//$("#error-msg-det").hide();
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

function closePopup() {
	$.modal.close();
}

function submitPopupForm(btn) {
	frm = $(btn).closest(".metro-container-300").find("[id^=editDreamForm]");
	if (!$(frm).valid()) {
		return false;
	}
	$.post($(frm).attr("action"), $(frm).serialize(), function (data) {
		if (data.success == true) {
			$.modal.close();
			document.location.reload(true);
		}
	});
	return false;
}