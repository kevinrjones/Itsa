// ReSharper disable InconsistentNaming
define(['durandal/system', 'services/logger', 'facades/signalr', 'i18n!nls/site', 'viewmodels/authentication'], function (system, logger, server, resources, authentication) {

    var id;
    var originalTitle = "";
    var originalEntry = "";
    var blogTitle = ko.observable();
    var blogEntry = ko.observable();
    var blogTags = ko.observableArray();
    var isSaveable = ko.computed(function () {
        return blogTitle() !== originalTitle || blogEntry() !== originalEntry;
    });

    var vm = {
        activate: activate,
        viewAttached: viewAttached,
        title: 'Edit',
        blogTitle: blogTitle,
        blogEntry: blogEntry,
        blogTags: blogTags,
        saveEditPostLabel: resources.savePostLabel,
        saveEditDraftPostLabel: resources.saveDraftPostLabel,
        cancelEditPostLabel: resources.cancelPostLabel,
        saveEdit: save,
        saveDraftEdit: saveDraft,
        cancelEdit: cancel,
        isSaveable: isSaveable,
        isCancelable: isSaveable
    };

    return vm;

    //#region Internal Methods
    function activate(params) {
        console.log("activate");
        id = params.id;
        originalTitle = "";
        originalEntry = "";
        cancel();
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
        console.log("viewAttached: getBlogPost");
        if (id != undefined && id != 0) {
            server.getBlogPost(id)
                .done(function(post) {
                    console.log("blog post title: " + post.Title);
                    originalTitle = post.Title;
                    blogTitle(post.Title);
                    originalEntry = post.Body;
                    blogEntry(post.Body);
                })
                .fail(function() {
                    handleSignalRFail();
                });
        }
        return true;
    }

    function toObject(isDraft) {
        return { Id: id, Title: blogTitle(), Body: blogEntry(), Tags: blogTags(), IsDraft: isDraft };
    }

    //#endregion

    function cancel() {
        blogTitle(originalTitle);
        blogEntry(originalEntry);
        blogTags([]);
    }

    function save() {
        if (isSaveable()) {
            server.updatePost(toObject(false)).
                done(function () {
                    // navigate to edit/list?
                }).fail(function () {
                    var app = require('durandal/app');
                    app.showMessage(resources.savePostTitle, resources.unableToSavePost);
                });
        }
    }
    function saveDraft() {
        if (isSaveable()) {
            server.updatePost(toObject(true)).
                done(function () {
                    // navigate to edit?/list?/home?
                }).fail(function () {
                    var app = require('durandal/app');
                    app.showMessage(resources.savePostTitle, resources.unableToSavePost);
                });
        }
    }

});


// ReSharper restore InconsistentNaming
