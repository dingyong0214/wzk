function ShowModelStateErrorMsg(obj, msg) {
    $('span[data-valmsg-for="' + obj + '"]')
        .removeClass('field-validation-valid')
        .addClass('field-validation-error')
        .html('<span for="' + obj + '" class="">' + msg + '</span>');
        //.show()
        //.delay(5000)
        //.hide();
}
function HideModelStateErrorMsg(obj) {
    $('span[data-valmsg-for="' + obj + '"]').addClass('field-validation-valid').removeClass('field-validation-error').html('');
}
function HideAllModelStateErrorMsg() {
    $('.field-validation-error').addClass('field-validation-valid').removeClass('field-validation-error').html('');
}
