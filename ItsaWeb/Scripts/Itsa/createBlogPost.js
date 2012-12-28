function CreateBlogPost(parent) {
    var self = this;
    self.parent = parent;

    self.postTitle = ko.observable();
    self.postBody = ko.observable();

    self.add = function () {
        $.connection.adminHub.addBlogPost({ title: self.postTitle(), post: self.postBody() })
            .done(function (post) {
                parent.postAdded(post);
                self.postTitle("");
                self.postBody("");
            })
            .fail(function () {
                parent.postAdded(null);
            });
    };
};
