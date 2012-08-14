function LanguageResultViewModel(js) { // expect input to be pojo
    var self = this;
    ko.mapping.fromJS(js, {}, self);

    self.href = ko.computed(function () { // use t4mvcjs to generate link href
        return MvcJs.Languages.Item.Get(self.TwoLetterIsoCode());
    });
    self.goToHref = function () { // used for navigating by clicking table row link
        location.href = self.href();
    };
    self.formattedNativeNameText = ko.computed(function () { // display null value
        if (self.NativeNameText() && self.NativeNameText() !== self.TranslatedNameText()) {
            return self.NativeNameText();
        }
        return '-';
    });
    self.isoCodes = ko.computed(function () {
        var isoCodes = self.TwoLetterIsoCode() + ', ' + self.ThreeLetterIsoCode();
        if (self.ThreeLetterIsoCode() !== self.ThreeLetterIsoBibliographicCode())
            isoCodes += ', ' + self.ThreeLetterIsoBibliographicCode() + '*';
        return isoCodes;
    });
    self.namesCountPluralizer = ko.computed(function () {
        var translationPluralizer = (self.NamesCount() == 1) ? 'translation' : 'translations';
        return self.NamesCount() + ' ' + translationPluralizer;
    });
}

function LanguageResultsViewModel() {
    var self = this;
    PageOfResultsViewModel.call(self);
    LayoutScrollViewModel.call(self);

    // search
    self.keyword = ko.observable(); // observe what is typed into search field
    self.throttledKeyword = ko.computed(self.keyword)
        .extend({ throttle: 400 }); // only update this when user has stopped typing

    // result items
    self.resultsMapping = {
        'Items': {
            key: function (item) {
                return ko.utils.unwrapObservable(item.Id);
            },
            create: function (options) {
                return new LanguageResultViewModel(options.data);
            }
        }
    };

    // ajax results update
    ko.computed(function () { // update the results by getting json from server (happens during first load)
        self.startSpinning();
        $.get(MvcJs.Languages.Search.Get(), { // get json from server
            keyword: self.throttledKeyword(), // use throttled keyword to trigger this event
            pageSize: self.pageSize(),
            pageNumber: self.pageNumber()
        })
        .success(function (response) {
            self.update(response);
            self.restoreScrollTop();
        });
    })
    .extend({ throttle: 1 });

    // ajax preference updates
    var savePreference = function(input, value) {
        var category = $(input).data('preference-category');
        var key = $(input).data('preference-key');
        if (!category || !key) return;
        $.ajax({
            url: MvcJs.Preferences.Change.Put(),
            type: 'PUT',
            data: {
                category: category,
                key: key,
                value: value
            }
        });
    };
    self.lens.subscribe(function (newValue) {
        savePreference(':input[data-bind*="value: lens"]', newValue);
    });
    self.pageSize.subscribe(function (newValue) {
        savePreference(':input[data-bind*="value: pageSize"]', newValue);
    });
}
