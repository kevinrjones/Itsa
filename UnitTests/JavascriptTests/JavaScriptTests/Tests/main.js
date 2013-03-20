QUnit.config.autostart = false;

require(['./blogPostTests', './resTests'], function () {
    QUnit.start(); //Tests loaded, run tests
});