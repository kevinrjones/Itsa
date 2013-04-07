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
            var routes = [{
                    url: 'home',
                    visible: true,
                    isActivate: ko.computed,
                    settings: {
                        needsNoAuthentication: ko.observable(true),
                        isAuthenticated: ko.observable(true),
                        icon: "nav-icon-home"
                    }
                }, {
                    url: 'new',
                    visible: true,
                    isActivate: ko.computed,
                    settings: {
                        needsNoAuthentication: ko.observable(false),
                        isAuthenticated: ko.observable(false),
                        icon: "nav-icon-new"
                    }
                }];

            router.map(routes);

            // this is the default route!
            server.isAuthenticated()
                .done(function (result) {
                    $.each(router.visibleRoutes(), function () {
                        this.settings.isAuthenticated(result);
                    });
                });

            return router.activate('home');
        }

        function log(msg, data, showToast) {
            logger.log(msg, data, system.getModuleId(shell), showToast);
        }
        //#endregion
    });