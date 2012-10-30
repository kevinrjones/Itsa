var admin = function () {

    
    function init() {
        $.connection.hub.start(function () {
            $.connection.userHub.getUserName()
                .done(function (data) {                    
                    if (data) {
                        var model = new gridViewModel(data);
                        ko.applyBindings(model);
                    } else {
                        // otherwise we're not logged in, so bounce to login page
                        redirectToLogin();
                    }
                });
        });
    }
    
    function redirectToLogin() {

        // notify user in Growl box
        $.jGrowl('Redirecting to login...');

        // then redirect
        window.location = '/Session/Index?redirectTo=' + encodeURIComponent(window.location);
    }

    function gridViewModel(params) {
        var model = this;

        model.userName = ko.observable(params.UserName);
        model.allowComments = ko.observable(params.AllowComments);
        model.ModerateComments  = ko.observable(params.ModerateComments);
        model.UserName  = ko.observable(params.UserName);
        model.BlogTitle  = ko.observable(params.BlogTitle);
        model.BlogSubTitle  = ko.observable(params.BlogSubTitle);
    };


    /* publicly-exposed methods */
    return {
        init: init
    };
}();

$(function () {
    admin.init();    
})