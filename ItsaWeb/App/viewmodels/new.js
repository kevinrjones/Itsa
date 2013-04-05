// ReSharper disable InconsistentNaming
define(['durandal/system', 'services/logger', 'facades/signalr'], function (system, logger, server) {

    var blogTitle = ko.observable();
    var blogEntry = ko.observable();
    var blogTags = ko.observableArray();

    var vm = {
        activate: activate,
        viewAttached: viewAttached,
        title: 'New',
        blogTitle: blogTitle,
        blogEntry: blogEntry,
        blogTags: blogTags,
    };

    return vm;

    //#region Internal Methods
    function activate() {
        return true;
    }
    
    function viewAttached() {
        return true;
    }
    //#endregion
});


// ReSharper restore InconsistentNaming
