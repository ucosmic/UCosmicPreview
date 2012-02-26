$(function () {
    $('a.not-implemented').click(function () {
        alert('This feature has not yet been implemented.');
    });
    $('#person_self_editor input[type=text]').blur(function () {
        $(this).val($.trim($(this).val()));
    });
    var displayNameConfig = $('[data-ucosmic-person-derive-display-name=true]');
    var displayNameId = displayNameConfig.data('ucosmic-person-derive-display-name-textbox');
    var isDisplayNameDerivedId = displayNameConfig.data('ucosmic-person-derive-display-name-checkbox');
    var originalValue = $('#' + displayNameId).val();
    if ($('#' + isDisplayNameDerivedId).is(':checked')) {
        $('#' + displayNameId).attr('readonly', 'readonly');
    }
    var autoDisplayName = function (fromCheckbox) {
        $('#' + displayNameId).removeAttr('readonly');
        if ($('#' + isDisplayNameDerivedId).is(':checked')) {
            $('#' + displayNameId).attr('readonly', 'readonly');
            originalValue = $('#' + displayNameId).val();
            $.ajax({
                url: displayNameConfig.data('ucosmic-person-derive-display-name-url'),
                type: 'POST',
                data: $('[data-ucosmic-person-derive-display-name=true]').closest('form').serialize(),
                success: function (model) {
                    $('#' + displayNameId).val($.trim(model.DisplayName));
                }
            });
        } else if (fromCheckbox) {
            $('#' + displayNameId).val(originalValue);
        }
    };

    var autoDisplayNameHandler = function (event, ui) {
        $(this).val(ui.item.value);
        $(this).parents('.combobox').find('.watermark').hide();
        autoDisplayName();
    };
    var comboboxOpenHandler = function () {
        var inputWrapper = $(this).parents('.text-box.input');
        var ul = $(this).parents('.combobox').find('.ui-autocomplete');
        var button = $(this).parents('.combobox').find('a.text-box.down-arrow');

        var oldLeft = ul.position().left;
        var leftAdjustment = parseInt(inputWrapper.css('padding-left'))
                + parseInt(inputWrapper.css('border-left-width'))
            ;
        var newLeft = oldLeft - leftAdjustment;
        ul.css('left', newLeft);

        var oldTop = ul.position().top;
        var topAdjustment = parseInt(inputWrapper.css('padding-bottom'));
        var newTop = oldTop + topAdjustment;
        ul.css('top', newTop);

        var oldWidth = ul.width();
        var widthAdjustment = parseInt(inputWrapper.css('padding-left'))
                + parseInt(button.width())
                + parseInt(button.css('border-left-width'));
        var newWidth = oldWidth + widthAdjustment;
        ul.css('width', newWidth);
    };
    $('#' + isDisplayNameDerivedId).click(function () {
        autoDisplayName(true);
    });
    $('[data-ucosmic-autocomplete=true]').each(function () {
        var config = $(this);
        var url = $(this).data('ucosmic-autocomplete-url');
        var input = $(this).find('input[type=text][id!=""][name!=""]');
        input.autocomplete({
            source: url,
            minLength: 0,
            appendTo: $(this).find('.autocomplete-menu'),
            focus: autoDisplayNameHandler,
            select: autoDisplayNameHandler,
            open: comboboxOpenHandler
        });
        $(this).find('a.text-box.down-arrow').click(function (e) {
            e.stopPropagation();
            $('[data-ucosmic-autocomplete=true]').each(function () {
                if (config[0] === $(this)[0]) return; // close others
                $(this).find('input[type=text][id!=""][name!=""]').autocomplete('close');
            });
            var isVisible = input.autocomplete('widget').is(':visible');
            if (isVisible) {
                input.autocomplete('close');
                return false;
            }
            if (e.currentTarget && e.currentTarget.tagName.toUpperCase() !== 'INPUT')
                $(this).blur();
            input.autocomplete('search', '');
            return false;
        });
        $(document).click(function () {
            input.autocomplete('close');
        });
    });
    $('#person_self_editor').closest('form').find('input[type=text]').on('keyup blur', function () {
        autoDisplayName();
    });
});
