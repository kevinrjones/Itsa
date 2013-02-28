function CreateBlogPost(params) {
    var self = this;
    self.applicationViewModel = params.applicationViewModel;

    self.postTitle = ko.observable();
    self.postBody = ko.observable();

    self.add = function () {
        $.connection.adminHub.server.addBlogPost({ title: self.postTitle(), post: self.postBody() })
            .done(function (post) {
                applicationViewModel.postAdded(post);
                self.postTitle("");
                self.postBody("");
            })
            .fail(function () {
                parent.postAdded(null);
            });
    };
};
