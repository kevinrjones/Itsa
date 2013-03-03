define(['durandal/system', 'services/logger', 'viewmodels/authentication'], function (system, logger, authentication) {

    var blogPosts = ko.observable();

    var vm = {
        activate: activate,
        title: 'Home View',
        blogPosts: blogPosts,
        isAuthenticated: authentication.isAuthenticated
    };

    return vm;

    //#region Internal Methods
    function activate() {
        logger.log('Home View Activated', null, 'home', true);
        $.connection.blogHub.server.getBlogEntries()
            .done(function (data) {
                //require(['viewmodels/blogPost'], function (post) {
                //    post.init(item, self);
                //    return post;
                //});
                var model = new BlogPostsModel(self);
                model.onPosts(data);
                blogPosts(model);
            })
            .fail()
            .always();
        return true;
    }
    

//#endregion
});

function BlogPostsModel(parent) {
    var self = this;
    self.parent = parent;

    self.posts = ko.observableArray([]);

    self.onPosts = function (data) {
        var returnedPosts = $.map(data, function (item) {
            var post = new BlogPost(item, self);
//            post.init(item, self);
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

function BlogPost(item, parent) {
    var self = this;
    self.parent = parent;

    self.id = ko.observable();
    self.postTitle = ko.observable();
    self.postBody = ko.observable();
    self.postCreated = ko.observable();
    self.postUpdated = ko.observable();

    if (item != null) {
        self.postTitle(item.Title);
        self.postBody(item.Post);
        self.postCreated(item.EntryAddedDate);
        self.postUpdated(item.EntryUpdatedDate);
        self.id(item.Id);
        console.log("home: blogPost");
    }

    self.deleteButtonTitle = resources.res("ItsaWeb.Resources.Resources.DeletePost");

    self.deletePost = function () {
        $.connection.adminHub.deleteBlogPost(self.id())
            .done(function (data) {
                self.parent.remove(self);
            })
            .fail(function (error) {
            });
    };
};