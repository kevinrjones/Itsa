define(['services/logger'], function (logger) {
    // ReSharper disable InconsistentNaming
    function BlogPost(item, parent) {
        var self = this;
        self.parent = parent;

        self.id = ko.observable();
        self.postTitle = ko.observable();
        self.postBody = ko.observable();
        self.postCreated = ko.observable();
        self.postUpdated = ko.observable();

        if (item != null) {
            self.postTitle(item.Title);
            self.postBody(item.Post);
            self.postCreated(item.EntryAddedDate);
            self.postUpdated(item.EntryUpdatedDate);
            self.id(item.Id);
            console.log("home: blogPost");
        }

        self.deleteButtonTitle = resources.res("ItsaWeb.Resources.Resources.DeletePost");

        self.deletePost = function () {
            $.connection.adminHub.deleteBlogPost(self.id())
                .done(function (data) {
                    self.parent.remove(self);
                })
                .fail(function (error) {
                });
        };
    };

    return BlogPost;

});

// ReSharper restore InconsistentNaming
