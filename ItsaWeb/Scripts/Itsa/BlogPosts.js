function BlogPostsModel() {
    var self = this;

    self.posts = ko.observableArray([]);

    self.onPosts = function (data) {

        var returnedPosts = $.map(data, function (item) {
            return new BlogPost(item, self);
        });

        self.posts(returnedPosts);
    };

    self.remove = function (post) {
        self.posts.remove(function (item) {
            return item.id() == post.id();
        });
    };
};