function userModel(params) {
    var self = this;

    self.name = ko.observable(params.Name);
    self.allowComments = ko.observable(params.AllowComments);
    self.ModerateComments = ko.observable(params.ModerateComments);
    self.UserName = ko.observable(params.UserName);
    self.BlogTitle = ko.observable(params.BlogTitle);
    self.BlogSubTitle = ko.observable(params.BlogSubTitle);
    self.isAuthenticated = ko.observable(params.IsAuthenticated);
    self.isCreatingBlogPost = ko.observable(false);

    self.addBlogEntry = function () {
        $.connection.adminHub.addEntry({});
    };


    self.addBlogPost = function () {
        self.showCreateBlogPost();
    };

    self.showCreateBlogPost = function () {
        self.isCreatingBlogPost(true);
    };

    self.newPost = new CreateBlogPost(this);
};



