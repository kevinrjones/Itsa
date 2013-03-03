define('blogPost', ['services/logger', 'viewmodels/details', 'viewmodels/authentication'], function (logger, details, authentication) {

    var id = ko.observable(),
    postTitle = ko.observable(),
    postBody = ko.observable(),
    postCreated = ko.observable(),
    postUpdated = ko.observable(),
    deleteButtonTitle = resources.res("ItsaWeb.Resources.Resources.DeletePost");

    var vm = {
        activate: activate,
        init: init,
        id: id,
        postTitle: postTitle,
        postBody: postBody,
        postCreated: postCreated,
        postUpdated: postUpdated,
        deleteButtonTitle: deleteButtonTitle
    };

    return vm;

    function activate(context) {
    }

    function init(item) {
        logger.log('blogPost Activated', null, 'home', true);

        if (item != null) {
            self.postTitle(item.Title);
            self.postBody(item.Post);
            self.postCreated(item.EntryAddedDate);
            self.postUpdated(item.EntryUpdatedDate);
            self.id(item.Id);
            console.log("blogPost");
        }
    }

    function deletePost() {
        $.connection.adminHub.deleteBlogPost(self.id())
            .done(function (data) {
                self.parent.remove(self);
            })
            .fail(function (error) {
            });
    };
});

