function ApplicationViewModel() {
    var self = this;

    self.allowComments = ko.observable();
    self.manageUser = ko.observable();
    
    self.init = function (params) {
        self.manageUser(new ManageUserViewModel());
        self.manageUser().init({ applicationViewModel: self });

        // load blog module        
        page.getPosts();

    };

    // todo: replace this with a reference to the UserModel, cf blogPosts below
    self.isCreatingBlogPost = ko.observable(false);
    self.message = ko.observable();

    self.signInText = ko.computed(function () {
        if (self.manageUser() && self.manageUser().isAuthenticated && self.manageUser().isAuthenticated()) {
            return resources.res('ItsaWeb.Resources.Resources.SignOut');
        } else {
            return resources.res('ItsaWeb.Resources.Resources.SignIn');
        }
    });

    self.showManageUserUi = function () {        
        self.manageUser().show();
    };

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
        if (self.isAuthenticated()) {
            $.connection.adminHub.server.addEntry({});
        } else {
            $.jGrowl("You are not allowed to post to this blog");
        }
    };


    self.addBlogPost = function () {
        self.showCreateBlogPost();
    };

    self.showCreateBlogPost = function () {
        self.isCreatingBlogPost(true);
    };

    self.newPost = new CreateBlogPost({ applicationViewModel: this });

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