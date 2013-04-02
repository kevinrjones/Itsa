define(['viewmodels/blogPost', 'facades/signalr','sinon'], function (BlogPost, server, sinon) {
    module("blogPost Tests", {
        setup: function () {
        }
    });

    test("blogpost delete text set", function () {
        var post = new BlogPost({ parent: this, item: { Title: "", Post: "", EntryAddedDate: new Date(), EntryUpdatedDate: new Date(), Id: 1 } });

        ok(post.deleteButtonTitle == "Delete Blog Post", "deleteButtonTitle: " + post.deleteButtonTitle + " should be Delete Blog Post");
    });

    test("uninitialized blogpost throws exception", function () {
        throws(function () { new BlogPost(); }, "Should trown an exception when not correctly initialized");
    });

    var post;
    var remove;
    var deleteBlogPost;

    function initialiseDeleteBlogPostCall() {
        var deferred;
        deferred = new $.Deferred();
        deleteBlogPost = sinon.stub();
        deleteBlogPost.returns(deferred);
        server.deleteBlogPost = deleteBlogPost;
        deferred.resolve();
    };

    module("blogPost callback Tests", {
        setup: function () {
            initialiseDeleteBlogPostCall();
            remove = sinon.spy();
            post = new BlogPost({ parent: { remove: remove }, item: { Title: "", Post: "", EntryAddedDate: new Date(), EntryUpdatedDate: new Date(), Id: 1 } });

        },
        teardown: function () {
        }
    });

    test("deleteBlogPost passes correct id", function () {
        post.deletePost();
        ok(deleteBlogPost.calledWith(1), "deletePost should be called with correct id");
    });

    test("deleteBlogPost calls remove if successful", function () {
        post.deletePost();
        ok(remove.called, "remove should be called if succesful");
    });
});
