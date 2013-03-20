define(['durandal/system', 'services/logger'],
    function (system, logger) {

        var email = ko.observable(),
            password = ko.observable(),
            isAuthenticated = ko.observable(false),
            message = ko.observable(""),
            isErrorVisible = ko.observable(false);

        var signInText = ko.computed(function () {
            if (isAuthenticated()) {
                return resources.res('ItsaWeb.Resources.Resources.SignOut');
            } else {
                return resources.res('ItsaWeb.Resources.Resources.SignIn');
            }
        });

        var vm = {
            activate: activate,
            signInText: signInText,
            signInButtonLabel: resources.res('ItsaWeb.Resources.Resources.SignIn'),
            signOutButtonLabel: resources.res('ItsaWeb.Resources.Resources.SignOut'),
            registerButtonLabel: resources.res('ItsaWeb.Resources.Resources.Register'),
            showManageUserUi: showManageUserUi,
            email: email,
            password: password,
            isAuthenticated: isAuthenticated,
            signIn: signIn,
            register: register,
            signOut: signOut,
            message: message,
            isErrorVisible: isErrorVisible
        };

        return vm;


        //#region Internal Methods

        function initialize(params) {
            isAuthenticated(true);
        }

        function showManageUserUi() {
            if ($('#manageUserUi').is(':visible')) {
                $('#manageUserUi').hide();
            } else {
                $('#manageUserUi').show();
            }
        }

        function activate() {
            system.log('authentication loaded', null, true);
        }

        function signIn() {
            $.post("/Session/Create", {
                email: email(),
                password: password()
            }, "json")
                .done(function (params) {
                    initialize(params);
                    $('#manageUserUi').hide();
                }).fail(function (error) {
                    message(resources.res('ItsaWeb.Resources.Resources.SignInError'));
                    isErrorVisible(true);
                    console.log(error);
                }).always(function () {
                });
        };

        function signOut() {
            $.post("/Session/Delete", "json")
                .done(function (params) {
                    initialize(params);
                }).fail(function (error) {
                    message("Unable to signin");
                    console.log(error);
                }).always(function () {
                });
        };

        function register() {
            $(id).hide();
        };


        //#endregion
    }
);