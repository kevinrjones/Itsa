ko.bindingHandlers.checkbox = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).checkbox();
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).checkbox("refresh");
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).datepicker();
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).datepicker("refresh");
    }
};


ko.bindingHandlers.expandingTextarea = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).expandingTextarea();
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).expandingTextarea("resize");
    }
};

ko.bindingHandlers.animate = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        if (valueAccessor()) {
            $(element).show();
        } else {
            $(element).hide();
        }
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        if (valueAccessor()) {
            $(element).slideDown(500, "linear");
        } else {
            $(element).slideUp(500, "linear");
        }
    }
};

ko.bindingHandlers.fadeOut = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);
        var delay = valueUnwrapped.delay ? valueUnwrapped.delay : 0;
        var fadeTime = valueUnwrapped.fadeTime ? valueUnwrapped.fadeTime : 500;

        $(element).delay(delay).fadeOut(fadeTime, function () {
            if (valueUnwrapped.flag) {
                valueUnwrapped.flag(false);
            }
        });
    }
};

//function oninit() {

//    // Disable caching of AJAX responses on localhost

//    // create new MasterViewModel, and bind using knockout
//    var masterModel = new MasterViewModel();
//    $.connection.hub.start(function () {
//        masterModel.oninit();
//    });
//}


//$(oninit);

