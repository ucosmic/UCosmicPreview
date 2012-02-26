$(function () {
    var dialogLanding = $('#dialog_landing');
    dialogLanding.dialog({
        title: 'Institutional agreement details',
        autoOpen: false,
        resizable: false,
        draggable: false,
        dialogClass: 'ucosmic modal',
        width: 'auto',
        height: 500,
        modal: true
    });
    $('a[data-ucosmic-dialog=960]').each(function () {
        $(this).on('click', function () {
            var url = $(this).attr('href');
            dialogLanding.html('')
                    .load(url, function () {
                        dialogLanding.dialog('open');
                        $.ucosmic.obtrude(dialogLanding);
                    });
            return false;
        });
    });

    var autoCompleteSelectHandler = function (event, ui) {
        $(this).val(ui.item.value);
        $(this).closest('form').submit();
    };
    var autoCompleteOpenHandler = function () {
        var inputWrapper = $(this).parents('.text-box.input');
        var button = $(this).parents('.emptybox').find('a.text-box.empty-icon');
        var ul = inputWrapper.find('.ui-autocomplete');

        var oldLeft = ul.position().left;
        var leftAdjustment = parseInt(inputWrapper.css('padding-left'))
                    + parseInt(inputWrapper.css('border-left-width'));
        var newLeft = oldLeft - leftAdjustment;
        ul.css('left', newLeft);

        var oldTop = ul.position().top;
        var topAdjustment = parseInt(inputWrapper.css('padding-bottom')) +
            parseInt(inputWrapper.css('border-bottom-width'));
        var newTop = oldTop + topAdjustment;
        ul.css('top', newTop);

        var oldWidth = ul.width();
        var widthAdjustment = parseInt(inputWrapper.css('padding-left'))
                + parseInt(button.width())
                + parseInt(button.css('border-left-width'))
                + parseInt(inputWrapper.css('border-left-width'))
                + parseInt(inputWrapper.css('border-right-width'));
        var newWidth = oldWidth + widthAdjustment;
        ul.css('width', newWidth);
    };
    $('[data-ucosmic-autocomplete=true]').each(function () {
        var url = $(this).data('ucosmic-autocomplete-url');
        var input = $(this).find('input[type=text]');
        input.autocomplete({
            //source: url
            source: function (request, response) {
                $.ajax({
                    url: url,
                    dataType: 'json',
                    type: 'POST',
                    traditional: true,
                    cache: true,
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.label,
                                value: item.value
                            };
                        }));
                        return null;
                    }
                });
            }
            , minLength: 1
            , appendTo: $(this).find('.autocomplete-menu')
            , select: autoCompleteSelectHandler
            , open: autoCompleteOpenHandler
        });

        var originalKeyword = input.val();

        input.parents('.emptybox').find('a.text-box.empty-icon').click(function () {
            input.val('');
            if (originalKeyword) input.closest('form').submit();
            else input.focus();
        });

        $(document).click(function () {
            input.autocomplete('close');
        });
    });
});
