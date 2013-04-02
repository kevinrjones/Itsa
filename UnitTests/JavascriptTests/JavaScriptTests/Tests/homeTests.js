define(['viewmodels/blogPosts', 'viewmodels/home', 'facades/signalr', 'sinon'], function(posts, home, server, sinon) {
    
    var getBlogEntries;
    
    module("blogEntries", {
        setup: function () {
            var deferred;

            getBlogEntries = sinon.stub();

            deferred = new $.Deferred();
            getBlogEntries.returns(deferred);

            server.getBlogEntries = getBlogEntries;
            server.isAuthenticated = function () { return new $.Deferred(); };
            deferred.resolve([{ Title: "kevin" }]);
        },
        teardown: function () {
        }
    });

    test("deleteBlogPost calls remove if successful", function () {
        home.activate();
        ok(getBlogEntries.called, "getBlogEntries should be called on activate");
        ok(posts, "posts is valid");
    });

});