$(document).ready(function () {
    $('.log-btn').click(function () {


        if ($("#UserName").val().trim() == "") {
            $('.log-status').addClass('wrong-entry');
            $('.alert').fadeIn(500);
            setTimeout("$('.alert').fadeOut(1500);", 3000);

            return false;
        }

        else if ($("#password").val().trim() == "") {
            $('.log-status1').addClass('wrong-entry');
            $('.alert').fadeIn(500);
            setTimeout("$('.alert').fadeOut(1500);", 3000);

            return false;
        }

        else if ($("#pin").val().trim() == "") {
            $('.log-status2').addClass('wrong-entry');
            $('.alert').fadeIn(500);
            setTimeout("$('.alert').fadeOut(1500);", 3000);

            return false;
        }

        return true;
    });


    $('#log-btn').click(function () {


        if ($("#UserName").val().trim() == "") {
            $('.log-status').addClass('wrong-entry');
            $('.alert').fadeIn(500);
            setTimeout("$('.alert').fadeOut(1500);", 3000);

            return false;
        }

        else if ($("#password").val().trim() == "") {
            $('.log-status1').addClass('wrong-entry');
            $('.alert').fadeIn(500);
            setTimeout("$('.alert').fadeOut(1500);", 3000);

            return false;
        }

        else if ($("#pin").val().trim() == "") {
            $('.log-status2').addClass('wrong-entry');
            $('.alert').fadeIn(500);
            setTimeout("$('.alert').fadeOut(1500);", 3000);

            return false;
        }

        else if ($("#pin").val().trim() != $("#checkpin").val().trim()) {
            $('.log-status2').addClass('wrong-entry');
            $('.alert').fadeIn(500);
            setTimeout("$('.alert').fadeOut(1500);", 3000);

            return false;
        }

        return true;
    });



    $('.form-control').keypress(function () {
        $('.log-status').removeClass('wrong-entry');
    });

});