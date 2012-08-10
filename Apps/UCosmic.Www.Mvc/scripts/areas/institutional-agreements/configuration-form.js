$(function () {

    //$('#institutional_agreement_configurator ul.listbox').css({ height: 'auto' });
    var configurationId = $('#institutional_agreement_configurator').attr('data-configuration-id');

    // return all elements needed to operate on appender LI
    function getAppenderItems(jqThis) {
        if (!jqThis || jqThis.length < 1)
            return null;
        var li = jqThis.parents('li.appender');
        return {
            li: li,
            ul: li.parents('ul'),
            editor: li.find('.editor'),
            editorValue: li.find('.editor .value'),
            editorInput: li.find('.editor .value input[type="text"]'),
            editorLast: li.find('.editor .value input[type="hidden"].last'),
            editorValidate: li.find('.editor .validate'),
            editorError: li.find('.editor .validate span[data-valmsg-replace="true"]'),
            editorAdd: li.find('.editor .actions .add'),
            editorEdit: li.find('.editor .actions .edit'),
            editorCancel: li.find('.editor .actions .cancel'),
            reader: li.find('.reader'),
            readerValue: li.find('.reader .value'),
            hiddenIsAdded: li.find('.hidden-input .is-added input[type="hidden"]'),
            form: li.parents('form'),
            comboBox: li.parents('fieldset').find('.combobox input[type="text"]'),
            radios: li.parents('fieldset').find('input[type="radio"].option-restriction')
        };
    }

    // operation for both the add and edit actions
    function saveAppenderValue(items) {
        if (items.editorInput.valid()) { // validate the input
            var duplicatesValidation = validateDuplicateOption(items); // special duplicates validation
            if (duplicatesValidation.IsValid) {
                items.editorLast.val(items.editorInput.val()); // store the user value in the hidden field
                items.readerValue.html(items.editorInput.val()); // show the user input in the reader
                items.editor.hide(); // hide the editor
                items.reader.show(); // show the reader
                items.readerValue.removeClass('mimic-textbox').addClass('mimic-textbox-changed')
                        .switchClass('mimic-textbox-changed', 'mimic-textbox', 2000); // animation effect
                alphabetize(items);
                updatePreview(items);
                return true;
            }
        }
        return false;
    }

    // fade in a new empty option item
    function loadNewAppender(items) {
        var url = items.ul.attr('data-new-value-url'); // get ajax url from html ul tag attribute
        $.ajax({
            url: url,
            cache: false,
            data: {
                configurationId: configurationId
            },
            success: function (html) {
                items.ul.prepend(html); // append the new li to the ul
                li = items.ul.find('li.appender .hidden-input .is-added input[value="False"]').parents('li.appender');
                li.hide(); // locate & hide the newly added li
                li.slideDown(200, function () { // fade in the newly added li
                    items.form.removeData('validator'); // enforce unobtrusive validation
                    items.form.removeData('unobtrusiveValidation');
                    $.validator.unobtrusive.parse(items.form);
                });
            }
        });
    }

    function alphabetize(items) {
        var ul = items.ul, lis = ul.find('li'), vals = [];
        lis.each(function () {
            $(this).attr('data-current-text', $(this).find('.editor .value input[type="text"]').val());
            vals.push($(this).attr('data-current-text'));
        });
        vals.sort(function (a, b) {
            var compA = a.toUpperCase(), compB = b.toUpperCase();
            return (compA < compB) ? -1 : (compA > compB) ? 1 : 0;
        });
        for (var i = 0; i < lis.length; i++)
            ul.append(ul.find('li[data-current-text="' + vals[i] + '"]').get());
        //ul.append(ul.find('li[data-current-text=""]').get());
    }

    // collect text values in the list
    function getTextValues(items) {
        var values = new Array(), index = 0;
        items.ul.find('li .editor').each(function () { // iterate over each LI editor element
            var liItems = getAppenderItems($(this)); // get the widget's items
            values[index++] = liItems.editorInput.val(); // store the user input in array
        });
        return values;
    }

    // validation to prevent entering of duplicate items
    function validateDuplicateOption(items) {
        // get all of the text that has already been entered
        var values = getTextValues(items);
        var validationResults = { // initialize the validation results
            IsValid: true,
            ErrorMessage: null
        };
        //return validationResults; // for testing server-side validation

        var url = items.ul.attr('data-validate-duplicate-url'); // get ajax url from html ul tag attribute
        var type = items.ul.attr('data-value-type');
        $.ajax({ // perform validation on server
            url: url,
            dataType: 'json',
            traditional: true,
            async: false,
            data: {
                type: type,
                values: values
            },
            success: function (data) {
                validationResults = data; // store server results
                if (!data.IsValid) { // show error to user if invalid
                    items.editorError.empty(); // remove other error messages
                    items.editorError.append($('<span></span>') // append the new error message
                                .text(data.ErrorMessage)
                                .attr('for', items.editorInput.attr('id'))
                                .attr('generated', 'true'));
                    items.editorError.removeClass('field-validation-valid'); // show the error message
                    items.editorError.addClass('field-validation-error'); // show the error message
                }
            },
            error: function () {
                validationResults = {
                    IsValid: false,
                    ErrorMessage: 'An unexpected error occurred, please try again.'
                };
            }
        });

        return validationResults;
    }

    // click to add
    $('#institutional_agreement_configurator ul.listbox li.appender .editor .actions .add a').live('click', function () {
        var items = getAppenderItems($(this));
        if (!items.editorValue.is(':visible')) { // show editor if not visible
            items.editorValue.slideDown(200); // slide down effect for editor
            items.editorCancel.fadeIn(200); // fade in effect for cancel button
            items.hiddenIsAdded.val('True'); // enforce validation
            items.editorInput.focus(); // put focus in the text box
        }
        else { // otherwise if valid, process add
            if (saveAppenderValue(items))
                loadNewAppender(items);
        }
        return false;
    });

    // click to cancel editor
    $('#institutional_agreement_configurator ul.listbox li.appender .editor .actions .cancel a').live('click', function () {
        var items = getAppenderItems($(this));
        if (items.editorAdd.is(':visible')) { // cancel the add
            items.editorValue.slideUp(200); // slide up effect for editor
            items.editorCancel.fadeOut(200); // fade out effect for cancel button
            items.hiddenIsAdded.val('False'); // enforce validation
        }
        else if (items.editorEdit.is(':visible')) { // cancel the edit
            items.editor.hide(); // hide the editor
            items.reader.show(); // show the reader
        }
        items.editorInput.val(items.editorLast.val()); // restore the last value from hidden field
        items.editorInput.valid(); // remove validation errors
        return false;
    });

    // click to edit
    $('#institutional_agreement_configurator ul.listbox li.appender .reader .actions .edit a, '
        + '#institutional_agreement_configurator ul.listbox li.appender .reader .value').live('click', function () {
            var items = getAppenderItems($(this));
            items.editorAdd.hide(); // hide the add button
            items.editorEdit.show(); // show the edit button
            items.reader.hide(); // hide the reader
            items.editor.show(); // show the editor
            items.editorInput.focus(); // put focus in the text box
            items.editorInput.select(); // select text in the text box
            return false;
        });

    // click to save edit
    $('#institutional_agreement_configurator ul.listbox li.appender .editor .actions .edit a').live('click', function () {
        var items = getAppenderItems($(this));
        saveAppenderValue(items);
        return false;
    });

    // click to remove
    $('#institutional_agreement_configurator ul.listbox li.appender .reader .actions .remove a').live('click', function () {
        var items = getAppenderItems($(this));
        items.li.slideUp(200, function () { // animate the removal
            items.li.remove(); // remove actual html/dom element
        });
        updatePreview(items);
        return false;
    });

    // remove empty items before submitting forms
    $('#institutional_agreement_configurator').parents('form').submit(function () {
        var valid = $(this).valid();
        if (!valid) return false;
        $(this).find('ul.listbox').each(function () { // for each listbox in the form
            var li = $(this).find('li.appender .hidden-input .is-added input[value="False"]').parents('li.appender');
            li.remove(); // find and remove the add-new li element so that model state is valid
        });
        return true;
    });

    // update combobox on radio button change
    $('input[type="radio"].option-restriction').change(function () {
        var items = getAppenderItems($(this).parents('fieldset').find('ul.listbox li.appender div:first'));
        updatePreview(items);
    });

    // update preview item
    function updatePreview(items) {
        var values = getTextValues(items);
        items.comboBox.parent().fadeOut(200);
        items.comboBox.val('[User input preview]');
        items.comboBox.autocomplete('option', 'source', values);
        items.radios.each(function () {
            if ($(this).is(':checked') && $(this).val() == 'True') {
                items.comboBox.removeAttr('readonly'); // make textbox editable
                items.comboBox.unbind('click');
            }
            else if ($(this).is(':checked') && $(this).val() == 'False') {
                items.comboBox.attr('readonly', 'readonly');
                items.comboBox.click(function (e) {
                    if (items.comboBox.autocomplete('widget').is(':visible')) {
                        items.comboBox.autocomplete('close');
                    }
                    else {
                        e.stopPropagation();
                        items.comboBox.autocomplete('search', '');
                    }
                });
            }
        });
        items.comboBox.parent().fadeIn(200);
    }
});
