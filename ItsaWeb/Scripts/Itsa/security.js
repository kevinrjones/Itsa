/* redirect the page to the login page */
var security = function() {
    
    function redirectToLogin() {

        // notify user in Growl box
        $.jGrowl('Redirecting to login...');

        // then redirect
        window.location = '/Session/Index?redirectTo=' + encodeURIComponent(window.location);
    }

    return {
        redirectToLogin: redirectToLogin
    };
}();