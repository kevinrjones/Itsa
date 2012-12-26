var admin = function () {

    
    function init() {
        $.connection.hub.start(function () {
            $.connection.userHub.getUser()
                .done(function (data) {
                    if (data) {
                        var model = new userModel(data);
                        ko.applyBindings(model, adminsection);
                    } else {
                        var model = new userModel({ isAuthenticated: false });
                        ko.applyBindings(model, adminsection);
                    }
                })
                .fail(function (error) {
                    console.log(error);
                    var model = new userModel({ isAuthenticated: false });
                    ko.applyBindings(model, adminsection);
                });
        });
    }
    
    function redirectToLogin() {

        // notify user in Growl box
        $.jGrowl('Redirecting to login...');

        // then redirect
        window.location = '/Session/New?redirectTo=' + encodeURIComponent(window.location);
    }



    /* publicly-exposed methods */
    return {
        init: init
    };
}();

