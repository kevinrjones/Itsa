var hash = function () {

    // called by hasher on first page load
    function handleHashInit(newHash) {

        console.log("handleHashInit: " + newHash);
        //// load home module first of all, telling it not to push its hash onto the url
        //var homeModule = 'meeting/list';

        //// if the url starts with 'meeting/list', let handleHashChange handle things (i.e. no need to pre-load the meeting list)
        //if (newHash.indexOf(homeModule) == 0) {

        //    handleHashChange(newHash);

        //} else {

        //    // otherwise we pre-load the home module, then when that's loaded we handle the initial hash
        //    modules.pushModule(homeModule, null, { dontPushHash: true }, function () {

        //        handleHashChange(newHash);

        //    });
        //}

    }

    // called every time the hash changes, e.g. on initial load, or browser history
    function handleHashChange(newHash, oldHash) {

        console.log("handleHashInit: " + newHash);
        //// ignore any blank hash
        //if (newHash == "")
        //    return;

        //// split the hash "path" into its component parts
        //var hashComponents = newHash.split('/');

        //// the module "id" is the first 2 components, e.g. "meeting/list"
        //var moduleId = hashComponents[0] + '/' + hashComponents[1];

        //// any further components are "query" parameters for the module to handle
        //var query = [];
        //for (var h = 2; h < hashComponents.length; h++)
        //    query.push(decodeURIComponent(hashComponents[h]));

        //var moduleAlreadyOnStack = modules.hasModule(moduleId);

        //console.log('Looking for ' + newHash + ' - ' + moduleId + ' on stack:' + moduleAlreadyOnStack);

        //// if the requested module is already on the stack, assume a "back", hence unwind to that module
        //if (moduleAlreadyOnStack) {

        //    while (!modules.isEmpty()) {

        //        // get the top module
        //        var topModule = modules.getTopModule();

        //        // if it's the same as the requested module, AND it supports a hashChange, AND chooses to do so, we're done
        //        if (topModule && topModule.moduleId == moduleId) {
        //            if (topModule.onHashChange && topModule.onHashChange(query)) {
        //                return;
        //            }
        //        }

        //        if (!modules.popModule()) {
        //            // disable hasher change notifications temporarily, so we don't get a feedback loop
        //            hasher.changed.active = false;

        //            // set the new hash
        //            hasher.setHash(oldHash);

        //            // re-enable hasher change notifications
        //            hasher.changed.active = true;

        //            return;
        //        }

        //    }
        //}

        //modules.pushModule(moduleId, null, { Query: query });

    }

    // called by modules when their internal state changes, and they wish this to be recorded on the url
    function pushHash() {
        //// join all passed arguments into a / separated "path"
        //var newHash = Array.prototype.join.call(arguments, '/');

        //// disable hasher change notifications temporarily, so we don't get a feedback loop
        //hasher.changed.active = false;

        //// set the new hash
        //hasher.setHash(newHash);

        //// re-enable hasher change notifications
        //hasher.changed.active = true;
    }

    // publicly-exposed methods
    return {
        handleHashInit: handleHashInit,
        handleHashChange: handleHashChange,
        pushHash: pushHash
    };
}();