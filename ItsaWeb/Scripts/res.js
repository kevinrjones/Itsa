/* "resources" module handles all localized resource strings, creating dictionary of key->value pairs in current language choice, populated on load of initial mvc page */

var resources = function () {

    /* js map containing all localized strings */
    var resources = {};

    /* add key-value pair to localized string map - called in base of SinglePageApp.cshtml */

    function setResource(key, value) {
        resources[key] = value;
    }

    /* retrieve localized string for a given key */

    function res(key, defaultValue) {
        var value = resources[key];
        if (!value) {
            if (arguments.length == 2)
                value = defaultValue;
            else
                value = key;
        }

        return value;
    }

    /* publicly-exposed methods */
    return {
        setResource: setResource,
        res: res
    };
}();