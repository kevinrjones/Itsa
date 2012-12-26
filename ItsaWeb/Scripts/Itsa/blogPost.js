function BlogPost(parent) {
    var self = this;
    self.parent = parent;

    self.postTitle = ko.observable();
    self.postBody = ko.observable();

    self.add = function () {
        $.connection.adminHub.addEntry({title: self.postTitle(), post: self.postBody()})
            .done(function () {
                $.jGrowl("Post added");
            })
            .fail(function () {
                $.jGrowl("Failed to add post");
            });
        parent.isCreatingBlogPost(false);
    };
};
