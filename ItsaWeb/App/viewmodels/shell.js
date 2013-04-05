define(['durandal/system', 'durandal/plugins/router', 'services/logger', 'facades/signalr'],
    function (system, router, logger, server) {
        var shell = {
            activate: activate,
            router: router,
            signInText: "Sign In",
            showManageUserUi: showManageUserUi
        };

        return shell;

        function showManageUserUi () {
            system.log("Show login");
        };

        //#region Internal Methods
        function activate() {
            return boot();
        }

        function boot() {
            //router.mapNav('details');
            var route = router.mapNav('home');
            route.needsNoAuthentication = ko.observable(true);
            route.isAuthenticated = ko.observable(true);
            route.icon = "nav-icon-home";

            route = router.mapNav('new');
            route.needsNoAuthentication = ko.observable(false);
            route.isAuthenticated = ko.observable(false);
            route.icon = "nav-icon-new";

            // this is the default route!
            server.isAuthenticated()
                .done(function (result) {
                    $.each(router.visibleRoutes(), function () {
                        this.isAuthenticated(result);
                    });
                });

            return router.activate('home');
        }

        function log(msg, data, showToast) {
            logger.log(msg, data, system.getModuleId(shell), showToast);
        }
        //#endregion
    });