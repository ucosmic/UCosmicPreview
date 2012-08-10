$(function () {
    $('a.others').on('click', function () {
        $('ul.others').slideToggle(200);
        return false;
    });

    $('[data-ucosmic-screenshot=dialog]').each(function () {
        var trigger = $(this).data('ucosmic-screenshot-trigger');
        var source = $('[data-ucosmic-screenshot-target=' + trigger + ']');
        var title = $(this).data('ucosmic-screenshot-title');
        var dialog = $(this);
        $(this).dialog({
            title: title,
            autoOpen: false,
            resizable: false,
            draggable: false,
            dialogClass: 'ucosmic modal',
            width: 920,
            height: 720,
            modal: true
        });
        source.on('click', function () {
            dialog.dialog('open');
        });
    });

//    var dialogLanding = $('#dialog_landing');
//    dialogLanding.dialog({
//        title: 'Institutional agreement details',
//        autoOpen: false,
//        resizable: false,
//        draggable: false,
//        dialogClass: 'ucosmic modal',
//        width: 'auto',
//        height: 500,
//        modal: true
//    });
//    $('a[data-ucosmic-dialog=960]').each(function () {
//        $(this).on('click', function () {
//            var url = $(this).attr('href');
//            dialogLanding.html('')
//                    .load(url, function () {
//                        dialogLanding.dialog('open');
//                        $.ucosmic.obtrude(dialogLanding);
//                    });
//            return false;
//        });
//    });

});