define(['viewmodels/blogPost', 'facades/signalr','sinon', 'require'], function (BlogPost, server, sinon, require) {
    module("blogPost Tests", {
        setup: function () {
        }
    });

    test("uninitialized blogpost throws exception", function () {
        throws(function () { var p = new BlogPost() }, "Should trown an exception when not correctly initialized");
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
        var app = require('durandal/app');
        var showMessageStub = sinon.stub();
        var deferred = new $.Deferred();
        deferred.resolve("Yes");
        showMessageStub.returns(deferred);
        app.showMessage = showMessageStub;
        
        post.deletePost();
        ok(deleteBlogPost.calledWith(1), "deletePost should be called with correct id");
    });

    test("deleteBlogPost calls remove if successful", function () {
        post.deletePost();
        ok(remove.called, "remove should be called if succesful");
    });
});
