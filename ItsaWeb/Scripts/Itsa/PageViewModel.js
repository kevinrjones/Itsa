PageViewModel = function (params) {
    var self = this;

    // todo: replace this with a reference to the UserModel, cf blogPosts below
    self.name = ko.observable(params.Name);
    self.allowComments = ko.observable(params.AllowComments);
    self.ModerateComments = ko.observable(params.ModerateComments);
    self.UserName = ko.observable(params.UserName);
    self.BlogTitle = ko.observable(params.BlogTitle);
    self.BlogSubTitle = ko.observable(params.BlogSubTitle);
    self.isAuthenticated = ko.observable(params.IsAuthenticated);
    self.isCreatingBlogPost = ko.observable(false);
    self.message = ko.observable();

    self.postAdded = function (post) {
        if (post != null) {
            self.message(resources.res('ItsaWeb.Resources.Resources.AddPostSucceeded'));
            $.jGrowl(resources.res('ItsaWeb.Resources.Resources.AddPostSucceeded'));
            self.isCreatingBlogPost(false);            
            self.blogPosts().posts.unshift(new BlogPost(post, self));
        } else {
            self.message(resources.res('ItsaWeb.Resources.Resources.AddPostFailed'));
            $.jGrowl(resources.res('ItsaWeb.Resources.Resources.AddPostFailed'));
        }
    };

    self.addBlogEntry = function () {
        $.connection.adminHub.server.addEntry({});
    };


    self.addBlogPost = function () {
        self.showCreateBlogPost();
    };

    self.showCreateBlogPost = function () {
        self.isCreatingBlogPost(true);
    };

    self.newPost = new CreateBlogPost(this);

    /* blogposts */
    self.getPosts = function () {
        $.connection.blogHub.server.getBlogEntries()
            .done(function (data) {
                var model = new BlogPostsModel(self);
                model.onPosts(data);
                self.blogPosts(model);
            }).fail(function() {
            });
    };

    self.blogPosts = ko.observable();
};