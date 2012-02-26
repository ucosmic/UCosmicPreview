$(function () {
    $('[data-app-sign-up-eligibility-url] input[type=button]').click(function () {
        var button = $(this);
        var container = $(this).parents('.check-eligibility');
        var form = $(this).parents('form');
        var wait = container.find('img.wait');
        var eligible = container.find('img.eligible');
        var url = container.data('app-sign-up-eligibility-url');
        var email = form.find('input[data-val-remote-url="' + url + '"]');
        var message = form.find('p .eligible');
        button.attr('disabled', 'disabled');
        eligible.hide();
        wait.show();
        var args = {
            emailAddress: email.val()
        };
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: args,
            complete: function () {
                form.valid();
                wait.hide();
                button.removeAttr('disabled');
            },
            success: function (data) {
                if (data === true && args.emailAddress) {
                    eligible.show();
                    message.slideDown(200);
                    var changedEmail = function () {
                        message.slideUp(200);
                        eligible.hide();
                    };
                    email.one('keydown', changedEmail);
                    email.one('change', changedEmail);
                    button.one('click', changedEmail);
                } else {
                    email.focus();
                    email.select();
                }
            }
        });
        return false;
    });
});
