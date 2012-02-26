$(function () {
    // add member
    $('#member_search').autocomplete({
        source: function (request, response) {
            var excludeUserEntityIds = [];
            $('#members_list li .entity-id input').each(function () {
                var isDeleted = $(this).parents('li').find('.is-deleted input').val();
                if (isDeleted && isDeleted !== 'False') return;
                var userId = $(this).val();
                if ($.inArray(userId, excludeUserEntityIds) < 0)
                    excludeUserEntityIds.push(userId);
            });
            var url = $('#members_list').data('ucosmic-autocomplete-url');
            $.ajax({
                url: url,
                dataType: 'json',
                type: 'POST',
                traditional: true,
                data: {
                    term: request.term,
                    excludeUserEntityIds: excludeUserEntityIds
                },
                success: function (data) {
                    if (!$('#member_search').val()) {
                        return null;
                    }
                    response($.map(data, function (item) {
                        return {
                            label: item.label,
                            value: item.value
                        };
                    }));
                    return null;
                }
            });
        },
        minLength: 1,
        appendTo: '.MemberSearch-field .autocomplete-menu',
        focus: function () {
            // do not change the text box value
            return false;
        },
        select: function (event, ui) {
            $(this).val('');
            var url = $('#members_list').data('ucosmic-add-url');
            $.ajax({
                url: url,
                data: {
                    userEntityId: ui.item.value
                },
                cache: false,
                success: function (html) {
                    if (!html) return;
                    $('#members_list li.empty').hide();
                    $('#members_list').append(html);
                }
            });
            return false;
        }
    });

    // remove member
    $('li.member a.remove-button').live('click', function () {
        var li = $(this).parents('li');
        li.find('.is-deleted input').val(true);
        li.slideUp(200, function () {
            showHideEmptyMembers();
        });
        return false;
    });

    // empty members
    function showHideEmptyMembers() {
        if ($('#members_list').find('li.member .hidden-input .is-deleted input[value="False"]').length > 0
                || $('#members_list').find('li.member .hidden-input .is-deleted input[value="false"]').length > 0) {
            $('#members_list li.empty').slideUp(200);
        }
        else {
            $('#members_list li.empty').fadeIn(200);
        }
    }
    showHideEmptyMembers();
});
