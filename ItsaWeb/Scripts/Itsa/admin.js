var application = function () {

    function getDefaultUser() {
        return {Name: '', ModerateComments: false, UserName: '', BlogTitle: '', BlogSubTitle: '', IsAuthenticated: false, AllowComments: false};
    }

    function init() {
        var page = new ApplicationViewModel();
        var user = getDefaultUser();
        $.connection.userHub.server.getUser()
            .done(function (data) {
                if (data) {
                    user  = data;
                } 
            })
            .fail(function (error) {
                console.log(error);
            }).always(function () {
                page.init(user);
                ko.applyBindings(page);
            });
    }

    /* publicly-exposed methods */
    return {
        init: init
    };
}();

$(function () {
    $.connection.hub.start(function () {
        application.init();
    });
});
