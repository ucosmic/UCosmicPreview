function LayoutScrollViewModel() {
    var self = this;

    var scrollPaneSelector = '.ucosmic .torso.row .center.col .scroll-y';
    $(scrollPaneSelector).attr('data-bind', 'event: { scroll: trackScrollTop }');

    self.scrollTop = ko.observable($(':input[data-bind*="value: scrollTop"]').val() || 0);
    self.trackScrollTop = function (viewModel, e) {
        self.scrollTop(e.currentTarget.scrollTop);
    };
    self.restoreScrollTop = function () {
        $(scrollPaneSelector).scrollTop(self.scrollTop());
    };
}
