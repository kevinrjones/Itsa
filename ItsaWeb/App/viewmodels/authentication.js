define(['durandal/system', 'durandal/plugins/router', 'services/logger', "i18n!nls/site", "facades/signalr"],
    function (system, router, logger, resources, server) {

        var email = ko.observable(),
            password = ko.observable(),
            isAuthenticated = ko.observable(false),
            message = ko.observable(""),
            isErrorVisible = ko.observable(false);

        var signInText = ko.computed(function () {
            if (isAuthenticated()) {
                return resources.signOut;
            } else {
                return resources.signIn;
            }
        });

        var vm = {
            activate: activate,
            signInText: signInText,
            signInButtonLabel: resources.signIn,
            signOutButtonLabel: resources.signOut,
            registerButtonLabel: resources.register,
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
            isAuthenticated(params.IsAuthenticated);
        }

        function updateRoutes(params) {
            $.each(router.visibleRoutes(), function () {
                this.settings.isAuthenticated(params.IsAuthenticated);
            });
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
                    updateRoutes(params);
                    $('#manageUserUi').hide();
                }).fail(function (error) {
                    message(resources.signInError);
                    isErrorVisible(true);
                    console.log(error);
                }).always(function () {
                });
        };

        function signOut() {
            $.post("/Session/Delete", "json")
                .done(function (params) {
                    initialize(params);
                    updateRoutes(params);
                    $('#manageUserUi').hide();
                    router.navigateTo('#home');
                }).fail(function (error) {
                    message("Unable to signout");
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