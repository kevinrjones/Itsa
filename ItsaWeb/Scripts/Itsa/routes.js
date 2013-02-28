var routes = function() {

    function initRoutes() {
        crossroads.routed.add(console.log, console); //log all routes
        crossroads.addRoute('/login', function (id) {
            console.log("login");
        });
        crossroads.addRoute('/logout', function (id) {
            console.log("logout");
        });
    }

    return {
        initRoutes: initRoutes
    };
}();