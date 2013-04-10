require.config({
    paths: {
        "text": "durandal/amd/text"
    }
});

requirejs.onError = function (err) {
    console.log(err.requireType);
    console.log('modules: ' + err.requireModules);
    throw err;
};

var editRoute = {
    url: 'edit/:id',
    visible: false,
    isActivate: ko.computed
};

var homePageRoutes = [{
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
}, {
    url: 'list',
    visible: true,
    isActivate: ko.computed,
    settings: {
        needsNoAuthentication: ko.observable(false),
        isAuthenticated: ko.observable(false),
        icon: ""
    }
}, editRoute];

var newPageRoutes = [{
    url: 'home',
    visible: true,
    isActivate: ko.computed,
    settings: {
        needsNoAuthentication: ko.observable(true),
        isAuthenticated: ko.observable(true),
        icon: "nav-icon-home"
    }
}, {
    url: 'list',
    visible: true,
    isActivate: ko.computed,
    settings: {
        needsNoAuthentication: ko.observable(false),
        isAuthenticated: ko.observable(false),
        icon: ""
    }
}, editRoute];

var listPageRoutes = [{
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
}, editRoute];


define(['durandal/app', 'durandal/viewLocator', 'durandal/system', 'durandal/plugins/router', 'services/logger'],
    function (app, viewLocator, system, router, logger) {

        // Enable debug message to show in the console 
        system.debug(true);
        app.title = "Itsa Knockout";

        app.start().then(function () {
            $.connection.hub.start(
                function () {

                    $.connection.hub.error(function (data) {
                        console.log('maybe the server is down', data);
                        var text = data.responseText;
                        $.jGrowl(text);
                    });

                    router.handleInvalidRoute = function (route, params) {
                        logger.logError('No Route Found', route, 'main', true);
                    };
                    router.onNavigationComplete = function (routeInfo, params, module) {
                        router.visibleRoutes([]);
                        if (!router.routeName) {
                            router.routeName = ko.observable();
                        }
                        router.routeName(routeInfo.name);
                        if (routeInfo.name == "New" || routeInfo.name == "Edit") {
                            router.map(newPageRoutes);
                        } else if (routeInfo.name == "List") {
                            router.map(listPageRoutes);
                        }
                        else if (routeInfo.name == "Home") {
                            router.map(homePageRoutes);
                        }
                        var server = require('facades/signalr');
                        server.isAuthenticated()
                            .done(function (result) {
                                $.each(router.visibleRoutes(), function () {
                                    this.settings.isAuthenticated(result);
                                });
                            });

                        if (app.title) {
                            document.title = app.title + " | " + routeInfo.caption;
                        } else {
                            document.title = routeInfo.caption;
                        }
                    };
                    // When finding a viewmodel module, replace the viewmodel string 
                    // with view to find it partner view.
                    router.useConvention();
                    viewLocator.useConvention();

                    // Adapt to touch devices
                    app.adaptToDevice();
                    //Show the app by setting the root view model for our application.
                    app.setRoot('viewmodels/shell', 'entrance');
                }
            );
            toastr.options.positionClass = 'toast-bottom-right';
            toastr.options.backgroundpositionClass = 'toast-bottom-right';

        });
    });

