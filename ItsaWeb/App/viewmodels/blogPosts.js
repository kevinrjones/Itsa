// ReSharper disable InconsistentNaming
define(['services/logger', 'viewmodels/blogPost', 'i18n!nls/site'], function (logger, blogPost, resources) {

    var posts = ko.observableArray([]);
    var _parent;

    var remove = function (post) {
        posts.remove(function (item) {
            return item.id() == post.id();
        });
        _parent.message(resources.PostDeleted);
    };

    function setParent(parent) {
        _parent = parent;
    }

    var onPosts = function (data) {
        var returnedPosts = $.map(data, function (item) {
            var post = new blogPost({ item: item, parent: vm });
            return post;
        });
        posts(returnedPosts);
    };

    var vm = {
        posts: posts,
        onPosts: onPosts,
        remove: remove,
        setParent: setParent
    };

    return vm;

});
// ReSharper restore InconsistentNaming
