function LanguageResultsViewModel(data) {
    data = data || {}; // make sure data is not undefined
    var self = this;

    self.Keyword = ko.observable(data.Keyword); // observe what is typed into search field
    self.ThrottledKeyword = ko.computed(self.Keyword) // only update this when user has stopped typing
                .extend({ throttle: 400 });

    function resultsViewModel(rawResults) { // add behaviors to the results
        $(rawResults).each(function () {
            var result = this;
            $.extend(result, {
                Href: ko.computed(function () { // use t4mvcjs to generate link href
                    return MvcJs.Languages.Language.Get(result.TwoLetterIsoCode);
                }),
                GoToHref: function () { // used for navigating by clicking table row link
                    window.location.href = result.Href();
                },
                FormattedNativeNameText: ko.computed(function () { // display null value
                    if (result.NativeNameText && result.NativeNameText !== result.TranslatedNameText) {
                        return result.NativeNameText;
                    }
                    return '-';
                }),
                IsoCodes: ko.computed(function () {
                    var isoCodes = result.TwoLetterIsoCode + ', ' + result.ThreeLetterIsoCode;
                    if (result.ThreeLetterIsoCode !== result.ThreeLetterIsoBibliographicCode)
                        isoCodes += ', ' + result.ThreeLetterIsoBibliographicCode + '*';
                    return isoCodes;
                }),
                NamesCountPluralizer: ko.computed(function () {
                    var translationPluralizer = (result.NamesCount == 1) ? 'translation' : 'translations';
                    return result.NamesCount + ' ' + translationPluralizer;
                })
            });
        });
        return rawResults;
    }
    self.Results = ko.observableArray(resultsViewModel(data.Results)); // initialize the results
    //self.ResultsWrapper = ko.observable({ Results: self.Results() });
    self.Results.subscribe(function (val) { // add behaviors to results whenever they change
        //var results =
        resultsViewModel(val);
        //self.ResultsWrapper({
        //    Results: results
        //});
    });
    self.HasResults = ko.computed(function () { // useful for showing/hiding table/no-results message
        return self.Results().length > 0;
    });
    self.PageNumber = ko.observable(data.PageNumber || 1);
    self.PageCount = ko.observable(data.PageCount || 1);
    self.NextPage = function () {
        if (self.PageNumber() < self.PageCount()) {
            self.PageNumber(self.PageNumber() + 1);
        }
    };
    self.PrevPage = function () {
        if (self.PageNumber() >= 1) {
            self.PageNumber(self.PageNumber() - 1);
        }
    };

    self.ResultsSize = ko.observable(data.ResultsSize || 10);

    self.IsLoadingResults = ko.observable(false); // true when in an ajax call
    self.IsSpinnerVisible = ko.observable(false); // delay the showing of this

    ko.computed(function () { // update the results by getting json from server (happens during first load)
        self.IsLoadingResults(true); // we are entering an ajax call
        setTimeout(function () { // delay the showing of the spinner
            if (self.IsLoadingResults()) { // only show it when load is still being processed
                self.IsSpinnerVisible(true);
            }
        }, 400); // delay the spinner this long
        $.get(MvcJs.Languages.Languages.Get(), { // get json from server
            Keyword: self.ThrottledKeyword(), // use throttled keyword to trigger this event
            Size: self.ResultsSize(),
            Number: self.PageNumber()
        })
        .success(function (response) { // server returns array only
            self.Results(response.Results); // update the ui
            self.PageNumber(response.PageNumber);
            self.PageCount(response.PageCount);
            self.IsLoadingResults(false); // loading is over
            self.IsSpinnerVisible(false); // hide the spinner
            self.RestoreScroll();
        });
    });

    self.ViewTemplates = ko.observableArray([
        { Text: 'Table' },
        { Text: 'List' },
        { Text: 'Grid' }
    ]);
    self.SelectedViewText = ko.observable(data.SelectedViewText || self.ViewTemplates()[0].Text);
    self.SelectView = function (item) {
        self.SelectedViewText(item.Text);
    };

    self.ScrollTop = ko.observable(data.ScrollTop || 0);
    self.TrackScroll = function (viewModel, e) {
        self.ScrollTop(e.currentTarget.scrollTop);
    };
    self.RestoreScroll = function () {
        $(data.ScrollTopSelector).scrollTop(self.ScrollTop());
    };
}
