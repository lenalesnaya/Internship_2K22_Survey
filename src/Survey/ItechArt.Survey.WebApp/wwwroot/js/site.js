$(document).ready(function () {

    $('#username').keypress(function NumericText() {
        var key = window.event.key;
        if (!((key >= '0' && key <= '9') || (key >= 'a' && key <= 'z') || (key >= 'а' && key <= 'я') ||
            (key >= 'A' && key <= 'Z') || (key >= 'А' && key <= 'Я') || key == '_' || key == ' ' ||
            key == 'ArrowLeft' || key == 'ArrowRight' || key == 'Delete' || key == 'Backspace'))
            window.event.returnValue = false;
    });

    $('#email, #password, #repeatpassword').on('input', function () {
        $(this).val($(this).val().replace(/[\s]/, ''))
    });

    $('#username, #email, #password, #repeatpassword').on("cut copy paste", function (e) {
        e.preventDefault();
    });

});