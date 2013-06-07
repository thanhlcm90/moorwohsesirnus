//CONTACT FORM
	$(document).ready(function(){
		$("#contact_form").validate({
			meta: "validate",
			submitHandler: function (form) {
				
				var s_name=$("#name").val();
				var s_lastname=$("#lastname").val();
				var s_email=$("#email").val();
				var s_phone=$("#phone").val();
				var s_comment=$("#comment").val();
				$.post("contact.php",{name:s_name,lastname:s_lastname,email:s_email,phone:s_phone,comment:s_comment},
			   	function(result){
                  $('#sucessmessage').append(result);
                });
				$('#contact_form').hide();
				return false;
			},
			/* */
			rules: {
				name: "required",
				
				lastname: "required",
				// simple rule, converted to {required:true}
				email: { // compound rule
					required: true,
					email: true
				},
				subject: {
					required: true,
				},
				comment: {
					required: true
				}
			},
			messages: {
				name: "Please enter your name.",
				lastname: "Please enter your last name.",
				email: {
					required: "Please enter email.",
					email: "Please enter valid email"
				},
				subject: "Please enter a subject.",
				comment: "Please enter a comment."
			},
		}); /*========================================*/
	});
