define(['viewmodels/blogPosts', 'durandal/app'], function (blogPosts) {
    module("blogPost Tests", {
        setup: function () {
            blogPosts.setParent({
                message: function () {
                }
            });
        }
    });

    test("correct item is removed from posts", function () {
        blogPosts.posts([{
            id: function () {
                return 1;
            }
        }, {
            id: function () {
                return 2;
            }
        }, {
            id: function () {
                return 3;
            }
        }]);
        blogPosts.remove({
            id: function () {
                return 2;
            }
        });
        ok(blogPosts.posts().length == 2, "An item is removed");
        ok(blogPosts.posts()[0].id() == 1, "First item is still there");
        ok(blogPosts.posts()[1].id() == 3, "Last item is still there");
    });

    test("correct items are added to posts", function () {
        blogPosts.onPosts(['a', 'b', 'c']);
        ok(blogPosts.posts().length == 3, "all items are added to posts");
    });
});
