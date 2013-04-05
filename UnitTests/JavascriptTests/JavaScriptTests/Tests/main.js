//define(function () {
//    var blockTest = require(['./blogPostsTests']);
//     blockTest = require(['./blogPostTests']);
//     blockTest = require(['./resTests']);
//     blockTest = require(['./signalRFacadeTests']);
//     blockTest = require(['./homeTests']);
//    QUnit.start(); //Tests loaded, run tests
//});
require(['./blogPostsTests', './blogPostTests', './resTests', './signalRFacadeTests', './homeTests'], function () {
    QUnit.start(); //Tests loaded, run tests
});