function PageOfResultsViewModel() {
    var self = this;

    // pager
    self.pageNumber = ko.observable($(':input[data-bind*="value: pageNumber"]').val() || 1);
    self.pageSize = ko.observable($(':input[data-bind*="value: pageSize"]').val() || 10);
    self.pageCount = ko.observable(1);
    self.nextPage = function () {
        if (self.pageNumber() < self.pageCount()) {
            self.pageNumber(self.pageNumber() + 1);
        }
    };
    self.prevPage = function () {
        if (self.pageNumber() >= 1) {
            self.pageNumber(self.pageNumber() - 1);
        }
    };
    self.nextEnabled = ko.computed(function () {
        return self.pageNumber() < self.pageCount();
    });
    self.prevEnabled = ko.computed(function () {
        return self.pageNumber() > 1;
    });

    // spinner
    self.isSpinning = ko.observable(true);
    self.showSpinner = ko.observable(false);
    self.spinnerDelay = ko.observable(400);
    self.startSpinning = function() {
        self.isSpinning(true); // we are entering an ajax call
        var currentSpinnerDelay = self.spinnerDelay();
        if (currentSpinnerDelay < 1)
            self.showSpinner(true);

        else
            setTimeout(function() { // delay the showing of the spinner
                if (self.isSpinning()) { // only show it when load is still being processed
                    self.showSpinner(true);
                }
            }, currentSpinnerDelay); // delay the spinner this long
    };
    self.stopSpinning = function() {
        self.showSpinner(false);
        self.isSpinning(false);
    };

    // result items
    self.Items = ko.observableArray([]);
    self.itemsMapping = { };
    self.update = function(js) {
        var mapped = ko.mapping.fromJS(js, self.itemsMapping);
        self.Items(mapped.Items());
        self.pageCount(mapped.PageCount());
        self.stopSpinning();
    };
    self.hasItems = ko.computed(function () {
        return self.Items().length > 0;
    });
    self.hasNoItems = ko.computed(function () { // useful for showing/hiding table/no-results message
        return !self.isSpinning() && !self.hasItems();
    });

    // result layout
    self.selectedLayout = ko.observable($(':input[data-bind*="value: selectedLayout"]').val() || 'Table');
    self.selectLayout = function (vm, e) {
        self.selectedLayout($(e.currentTarget).data('option-value'));
    };
    self.showTableLayout = ko.computed(function () {
        return self.selectedLayout() === 'Table' && self.hasItems();
    });
    self.showListLayout = ko.computed(function () {
        return self.selectedLayout() === 'List' && self.hasItems();
    });
    self.showGridLayout = ko.computed(function () {
        return self.selectedLayout() === 'Grid' && self.hasItems();
    });
    self.showMapLayout = ko.computed(function () {
        return self.selectedLayout() === 'Map' && self.hasItems();
    });
}
