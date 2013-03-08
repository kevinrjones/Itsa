// ReSharper disable InconsistentNaming
define(['services/logger', 'viewmodels/blogPost'], function (logger, BlogPost) {
    function BlogPosts(parent) {
        var self = this;
        self.parent = parent;

        self.posts = ko.observableArray([]);

        self.onPosts = function (data) {
            var returnedPosts = $.map(data, function (item) {
                var post = new BlogPost(item, self);
                return post;
            });
            self.posts(returnedPosts);
        };

        self.remove = function (post) {
            self.posts.remove(function (item) {
                return item.id() == post.id();
            });
            self.parent.message(resources.res('ItsaWeb.Resources.Resources.PostDeleted'));
        };
    };

    return BlogPosts;

});
// ReSharper restore InconsistentNaming
