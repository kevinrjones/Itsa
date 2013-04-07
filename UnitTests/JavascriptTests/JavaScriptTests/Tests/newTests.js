define(['viewmodels/new', 'facades/signalr', 'sinon'], function(newViewModel, server, sinon) {
    
    var createPost;
    
    module("new", {
        setup: function () {

            newViewModel.blogTitle("title");
            newViewModel.blogEntry("entry");
            var createPostDeffered;

            createPost = sinon.stub();

            createPostDeffered = new $.Deferred();
            createPost.returns(createPostDeffered);

            server.createPost = createPost;
            server.isAuthenticated = function () { return new $.Deferred(); };
            createPostDeffered.resolve({ Title: "title", Post: "post", Id: 1, EntryAddedDate: Date.parse("2013-04-07"), EntryUpdatedDate: Date.parse("2013-04-07"), CommentsEnabled: true, Draft: true });
        },
        teardown: function () {
        }
    });

    test("createPost calls create if a post is created", function () {
        newViewModel.save();
        ok(createPost.called, "create should be called when a post is created");
    });

});