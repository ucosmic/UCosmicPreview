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

    // update results
    self.items = ko.observableArray([]);
    self.resultsMapping = { };
    self.update = function (js) {
        ko.mapping.fromJS(js, self.resultsMapping, self);
        self.items(self.Items());
        self.pageCount(self.PageCount());
        self.stopSpinning();
    };
    self.hasItems = ko.computed(function () {
        return self.items().length > 0;
    });
    self.hasNoItems = ko.computed(function () { // useful for showing/hiding table/no-results message
        return !self.isSpinning() && !self.hasItems();
    });

    // result lens
    self.lens = ko.observable($(':input[data-bind*="value: lens"]').val() || 'Table');
    self.changeLens = function (vm, e) {
        self.lens($(e.currentTarget).data('option-value'));
    };
    self.showTableLens = ko.computed(function () {
        return self.lens() === 'Table' && self.hasItems();
    });
    self.showListLens = ko.computed(function () {
        return self.lens() === 'List' && self.hasItems();
    });
    self.showGridLens = ko.computed(function () {
        return self.lens() === 'Grid' && self.hasItems();
    });
    self.showMapLens = ko.computed(function () {
        return self.lens() === 'Map' && self.hasItems();
    });
}
