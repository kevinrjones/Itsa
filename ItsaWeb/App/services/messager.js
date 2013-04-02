;(function(define) {
    define(function() {
        return (function() {

            function init() {
                toastr.options.positionClass = 'toast-top-right';
                toastr.options.backgroundpositionClass = 'toast-top-right';
                toastr.options.timeOut = 1000;
            }

            var messager = {
                init: init,
                //messager: toastr,
                //error: toastr.error,
                //info: toastr.info
                messager: $.jGrowl,
                error: $.jGrowl,
                info: $.jGrowl
            };

            return messager;
        })();
    });
}(typeof define === 'function' && define.amd ? define : function(deps, factory) {
    if (typeof module !== 'undefined' && module.exports) { //Node
        module.exports = factory(require());
    } else {
        window['messager'] = factory();
    }
}));