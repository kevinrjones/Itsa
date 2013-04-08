define(['viewmodels/new', 'facades/signalr', 'sinon', 'require'], function(newViewModel, server, sinon, require) {
    
    var createPost;
    
    module("new post save succeeds", {
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

    test("createPost calls create if a post is created with save", function () {
        newViewModel.save();
        ok(createPost.called, "create should be called when a post is created");
    });

    test("createPost calls create if a post is created with saveDraft", function () {
        newViewModel.saveDraft();
        ok(createPost.called, "create should be called when a post is created");
    });

    test("createPost is passed the correct values when save is called", function () {
        newViewModel.save();
        var result = createPost.getCall(0).args[0];
        ok(result.Title == "title", "title should be 'title'");
        ok(result.Body == "entry", "entry should be 'entry'");
        ok(result.IsDraft === false, "isDraft should be 'false'");
    });

    test("cancel should reset the values of the view model", function () {
        newViewModel.cancel();
        ok(newViewModel.blogTitle() == "", "title should be empty");
        ok(newViewModel.blogEntry() == "", "entry should be empty");
        ok(newViewModel.blogTags().length == 0, "tags should be empty");
    });

    test("createPost is passed the correct values when saveDraft is called", function () {
        newViewModel.saveDraft();
        var result = createPost.getCall(0).args[0];
        ok(result.Title == "title", "title should be 'title'");
        ok(result.Body == "entry", "entry should be 'entry'");
        ok(result.IsDraft === true, "isDraft should be 'true'");
    });

    module("Is new post saveable");

    test("new post can not be saved when title and entry are empty", function () {
        newViewModel.blogTitle("");
        newViewModel.blogEntry("");
        ok(newViewModel.isSaveable() == false, "should not be able to save post when there is no entry");
    });
    
    test("new post can not be saved when entry is empty", function () {
        newViewModel.blogTitle("");
        ok(newViewModel.isSaveable() == false, "should not be able to save post when there is no title");
    });

    test("new post can  be saved when both entry and title have values", function () {
        newViewModel.blogTitle("");
        ok(newViewModel.isSaveable() == false, "should be able to save post when there is a title and entry");
    });

    module("new post save fails", {
        setup: function () {

            newViewModel.blogTitle("title");
            newViewModel.blogEntry("entry");
            var createPostDeffered;

            createPost = sinon.stub();

            createPostDeffered = new $.Deferred();
            createPost.returns(createPostDeffered);

            server.createPost = createPost;
            server.isAuthenticated = function () { return new $.Deferred(); };
            createPostDeffered.reject();
        },
        teardown: function () {
        }
    });
    
    test("failing to save post shows a message", function () {
        var app = require('durandal/app');
        var showMessageSpy = sinon.spy();
        app.showMessage = showMessageSpy;

        newViewModel.save();
        ok(showMessageSpy.called, "message box should be shown when call fails");
    });


});