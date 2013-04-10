// ReSharper disable InconsistentNaming
define(['durandal/system', 'services/logger', 'i18n!nls/site', 'durandal/plugins/router', 'viewmodels/new', 'viewmodels/edit'], function (system, logger, resources, router, newPost, editPost) {

    var dummyAction = {
        isSaveable: function () { return false; },
        isCancelable: function () { return false; },
        cancel: function () { return false; },
        save: function () { return false; },
        saveDraft: function () { return false; },
    };

    var action;
    var isSaveable = ko.computed(function () {
        if (router.routeName() == "New") {
            action = newPost;
        } else if (router.routeName() == "Edit") {
            action = editPost;
        } else {
            action = dummyAction;
        }
        return action.isSaveable();
    });

    var isCancelable = ko.computed(function () {
        if (router.routeName() == "New") {
            action = newPost;
        } else if (router.routeName() == "Edit") {
            action = editPost;
        } else {
            action = dummyAction;
        }
        return action.isCancelable();
    });

    function cancel() {
        if (router.routeName() == "New") {
            action = newPost;
        } else if (router.routeName() == "Edit") {
            action = editPost;
        } else {
            action = dummyAction;
        }
        action.cancel();
    }

    function save() {
        if (router.routeName() == "New") {
            action = newPost;
        } else if (router.routeName() == "Edit") {
            action = editPost;
        } else {
            action = dummyAction;
        }
        action.save();
    }

    function saveDraft() {
        if (router.routeName() == "New") {
            action = newPost;
        } else if (router.routeName() == "Edit") {
            action = editPost;
        } else {
            action = dummyAction;
        }
        action.saveDraft();
    }

    var vm = {
        activate: activate,
        savePostLabel: resources.savePostLabel,
        saveDraftPostLabel: resources.saveDraftPostLabel,
        cancelPostLabel: resources.cancelPostLabel,
        save: save,
        saveDraft: saveDraft,
        cancel: cancel,
        isSaveable: isSaveable,
        isCancelable: isCancelable
    };

    return vm;

    //#region Internal Methods
    function activate() {
        return true;
    }

    //#endregion
});


// ReSharper restore InconsistentNaming
