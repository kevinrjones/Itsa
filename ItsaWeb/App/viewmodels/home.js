// ReSharper disable InconsistentNaming
define(['durandal/system', 'services/logger', 'viewmodels/authentication', 'viewmodels/blogPosts', 'facades/signalr'], function (system, logger, authentication, model, server) {

    var blogPosts = ko.observable();

    var vm = {
        activate: activate,
        viewAttached: viewAttached,
        title: 'Home',
        blogPosts: blogPosts,
        isAuthenticated: authentication.isAuthenticated,
    };

    return vm;

    //#region Internal Methods
    function activate() {
        //logger.log('Home View Activated', null, 'home', true);
        server.getBlogEntries()
            .done(function (data) {
                model.setParent(vm);
                model.onPosts(data);
                blogPosts(model);
            })
            .fail(function (data) {
                handleSignalRFail(data);
            })
            .always();

        server.isAuthenticated()
            .done(function (result) {
                authentication.isAuthenticated(result);
            });

        return true;
    }
    
    function viewAttached() {
        $('pre code').each(function () { hljs.highlightBlock(this); });
        return true;
    }
    //#endregion
});


// ReSharper restore InconsistentNaming
