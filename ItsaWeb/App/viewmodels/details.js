define(['services/logger'], function (logger) {
    var vm = {
        activate: activate,
        viewAttached: viewAttached,
        title: 'Details View'
    };

    return vm;

    //#region Internal Methods
    function activate() {
        logger.log('Details View Activated', null, 'details', true);
        return true;
    }

    function viewAttached() {
        SyntaxHighlighter.all();
        logger.log('Details View Attached', null, 'details', true);
        return true;
    }

//#endregion
});