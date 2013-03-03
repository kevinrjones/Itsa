define(['durandal/system', 'durandal/plugins/router', 'services/logger'],
    function (system, router, logger) {
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
            router.mapNav('home');
            log('ITSA Knockout Loaded!', null, true);
            
            // this is the default route!
            return router.activate('home');
        }

        function log(msg, data, showToast) {
            logger.log(msg, data, system.getModuleId(shell), showToast);
        }
        //#endregion
    });