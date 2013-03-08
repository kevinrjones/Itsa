// ReSharper disable InconsistentNaming
define(['durandal/system', 'services/logger', 'viewmodels/authentication', 'viewmodels/blogPosts'], function (system, logger, authentication, BlogPosts) {

    var blogPosts = ko.observable();

    var vm = {
        activate: activate,
        title: 'Home View',
        blogPosts: blogPosts,
        isAuthenticated: authentication.isAuthenticated
    };

    return vm;

    //#region Internal Methods
    function activate() {
        logger.log('Home View Activated', null, 'home', true);
        $.connection.blogHub.server.getBlogEntries()
            .done(function (data) {
                var model = new BlogPosts(self);
                model.onPosts(data);
                blogPosts(model);
            })
            .fail()
            .always();
        return true;
    }
    //#endregion
});


// ReSharper restore InconsistentNaming
