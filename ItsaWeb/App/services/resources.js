/* "resources" module handles all localized resource strings, creating dictionary of key->value pairs in current language choice, populated on load of initial mvc page */
(function (define) {
    define([], function () {

        var resourceSingleton = function () {

            var resources = {};

            /* js map containing all localized strings */

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
        };
        
        return resourceSingleton();
    });
}(typeof define === 'function' && define.amd ? define : function (deps, factory) {
    if (typeof module !== 'undefined' && module.exports) { //Node
        module.exports = factory(require(deps[0]));
    } else {
        window['resources'] = factory();
    }
}));
