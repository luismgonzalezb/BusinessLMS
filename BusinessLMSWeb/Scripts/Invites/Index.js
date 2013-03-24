$(document).ready(function () {
	$("#info-modal").dialog({
		autoOpen: false,
		show: "blind",
		height: 600,
		width: 800,
		modal: true
	});
	$('#invites-table').dataTable({
		"bJQueryUI": true,
		"sPaginationType": "full_numbers"
	});
});

FB.init({ appId: "177099742414611", status: true, cookie: true });

function redeem(desc, url) {

	var obj = {
		method: 'feed',
		//redirect_uri: 'http://businesslms.localhost',
		link: url,
		name: desc,
		description: ''
	};

	function callback(response) {
		
	}
	FB.ui(obj, callback);

}