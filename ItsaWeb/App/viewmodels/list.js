// ReSharper disable InconsistentNaming
define(['durandal/system', 'services/logger', 'viewmodels/authentication', 'viewmodels/blogPosts', 'facades/signalr'], function (system, logger, authentication, model, server) {

    var blogPosts = ko.observable();

    var vm = {
        activate: activate,
        viewAttached: viewAttached,
        title: 'Home',
        blogPosts: blogPosts,
        isAuthenticated: authentication.isAuthenticated,
        message: function() {
        }
    };

    return vm;

    //#region Internal Methods
    function activate() {
        server.isAuthenticated()
            .done(function (result) {
                authentication.isAuthenticated(result);
                if (!authentication.isAuthenticated()) {
                    var router = require('durandal/plugins/router');
                    router.navigateTo('#home');
                }
            });

        server.getAllBlogEntries()
            .done(function (data) {
                model.setParent(vm);
                model.onPosts(data);
                blogPosts(model);
            })
            .fail(function (data) {
                handleSignalRFail(data);
            })
            .always();


        return true;
    }
    
    function viewAttached() {
        $('pre code').each(function (i, e) { hljs.highlightBlock(e); });
        return true;
    }
    //#endregion
});


// ReSharper restore InconsistentNaming
