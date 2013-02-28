function ManageUserViewModel() {

    var self = this;
    var id = '#manageUserUi';

    self.email = ko.observable();
    self.password = ko.observable();
    self.name = ko.observable();
    self.moderateComments = ko.observable();
    self.allowComments = ko.observable();
    self.userName = ko.observable();
    self.blogTitle = ko.observable();
    self.blogSubTitle = ko.observable();
    self.isAuthenticated = ko.observable(false);

    self.init = function (params) {
        self.applicationViewModel = params.applicationViewModel;
        $.connection.userHub.server.getUser()
            .done(function (data) {
                if (data != null) {
                    self._initialize(data);
                }
            }).fail(function (error) {
                $.jGrowl("Unable to signin");
                console.log(error);
            }).always(function () {
            });
    };

    self.show = function () {
        $(id).show();
    };

    self._initialize = function (params) {
        self.name(params.Name);
        self.moderateComments(params.ModerateComments);
        self.userName(params.UserName);
        self.blogTitle(params.BlogTitle);
        self.blogSubTitle(params.BlogSubTitle);
        self.isAuthenticated(params.IsAuthenticated);
        self.allowComments(params.AllowComments);
    };

    self.signIn = function () {
        $.post("/Session/Create", {
            email: self.email(),
            password: self.password()
        }, "json")
            .done(function (params) {
                self._initialize(params);
            }).fail(function (error) {
                $.jGrowl("Unable to signin");
                console.log(error);
            }).always(function () {
            });
    };

    self.signOut = function () {
        $.post("/Session/Delete", "json")
            .done(function (params) {
                self._initialize(params);
            }).fail(function (error) {
                $.jGrowl("Unable to signin");
                console.log(error);
            }).always(function () {
            });
    };
    self.register = function () {
        $(id).hide();
    };
}