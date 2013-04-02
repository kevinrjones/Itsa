/// <reference path="../Scripts/jquery-1.4.4-vsdoc.js" />
/// <reference path="Scripts/qunit.js" />
define(['res'], function (resources) {
    module("resource Tests", {
        setup: function () {
        }
    });

    test("add a resource to the resources", function () {
        resources.setResource('testKey', 'Test Value');

        equal(resources.res('testKey'), 'Test Value');
    });
});
