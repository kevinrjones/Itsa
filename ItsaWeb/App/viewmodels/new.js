// ReSharper disable InconsistentNaming
define(['durandal/system', 'services/logger', 'facades/signalr', 'i18n!nls/site', 'viewmodels/authentication'], function (system, logger, server, resources, authentication) {

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
        savePostLabel: resources.savePostLabel,
        saveDraftPostLabel: resources.saveDraftPostLabel,
        cancelPostLabel: resources.cancelPostLabel,
        save: save,
        saveDraft: saveDraft,
        cancel: cancel
    };

    return vm;

    //#region Internal Methods
    function activate() {
        // check authentication?
        // if not, cancel nav
        server.isAuthenticated()
            .done(function (result) {
                authentication.isAuthenticated(result);
                if (!authentication.isAuthenticated()) {
                    var router = require('durandal/plugins/router');
                    return router.navigateTo('#home');
                }
            });
        
        return true;
    }

    function viewAttached() {
        return true;
    }

    function toObject(isDraft) {
        return { title: blogTitle, entry: blogEntry, tags: blogTags, isDraft: isDraft };
    }

    //#endregion

    function cancel() {
        blogTitle("");
        blogEntry("");
        blogTags([]);
    }

    function save() {
        server.createPost(toObject(false)).
            done(function () {
                // navigate to edit/list?
            });

    }
    function saveDraft() {
        server.createPost(toObject(true).
            done(function () {
                // navigate to edit?/list?/home?
            }));

    }

});


// ReSharper restore InconsistentNaming
