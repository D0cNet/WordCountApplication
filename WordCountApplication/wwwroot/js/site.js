// Write your JavaScript code.
$(document).ready(function () {
	
	//Generate model and call index action

			
		$("#jsSubmit").on('click', function () {
			var PostUrl = $('#urlInput').val();
			if ($('#urlInput').val() != "") {
				
				$.ajax({
					type: "POST",
					url: "/Home/MainBody",
					beforeSend: function (xhr) {
						xhr.setRequestHeader("XSRF-TOKEN",
							$('input:hidden[name="__RequestVerificationToken"]').val());
					},
					data: $('#indexfrm').serialize(), 
					contentType: "application/x-www-form-urlencoded; charset=utf-8",
					dataType: "html",
					//Use data returned to populate "mainBody" div
					success: function (response) {
						
						var mBody = $("#mainBody");
						mBody.empty();
						mBody.html(response);

					},
					failure: function (response) {
						alert(response);
					}
			
				});
			}
		})

});