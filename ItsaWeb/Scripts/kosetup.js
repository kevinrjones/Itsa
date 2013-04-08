(function ($) {
    var instances_by_id = {} // needed for referencing instances during updates.
      , init_queue = $.Deferred() // jQuery deferred object used for creating TinyMCE instances synchronously
      , init_queue_next = init_queue;
    init_queue.resolve();
    ko.bindingHandlers.tinymce = {
        init: function (element, valueAccessor, allBindingsAccessor, context) {
            var init_arguments = arguments;
            var options = allBindingsAccessor().tinymceOptions || {};
            var modelValue = valueAccessor();
            var value = ko.utils.unwrapObservable(valueAccessor());
            var el = $(element);

            options.setup = function (ed) {
                ed.onChange.add(function (editor, l) { //handle edits made in the editor. Updates after an undo point is reached.
                    if (ko.isWriteableObservable(modelValue)) {
                        modelValue(l.content);
                    }
                });

                //This is required if you want the HTML Edit Source button to work correctly
                ed.onBeforeSetContent.add(function (editor, l) {
                    if (ko.isWriteableObservable(modelValue)) {
                        modelValue(l.content);
                    }
                });

                ed.onPaste.add(function (editor, evt) { // The paste event for the mouse paste fix.
                    var doc = editor.getDoc();

                    if (ko.isWriteableObservable(modelValue)) {
                        setTimeout(function () { modelValue(editor.getContent({ format: 'raw' })); }, 10);
                    }

                });

                ed.onInit.add(function (editor, evt) { // Make sure observable is updated when leaving editor.
                    var doc = editor.getDoc();
                    tinymce.dom.Event.add(doc, 'blur', function (e) {
                        if (ko.isWriteableObservable(modelValue)) {
                            modelValue(editor.getContent({ format: 'raw' }));
                        }
                    });
                });

            };

            //handle destroying an editor (based on what jQuery plugin does)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).parent().find("span.mceEditor,div.mceEditor").each(function (i, node) {
                    var tid = node.id.replace(/_parent$/, '');
                    var ed = tinyMCE.get(tid);
                    if (ed) {
                        ed.remove();
                        // remove referenced instance if possible.
                        if (instances_by_id[tid]) {
                            delete instances_by_id[tid];
                        }
                    }
                });
            });

            // TinyMCE attaches to the element by DOM id, so we need to make one for the element if it doesn't have one already.
            if (!element.id) {
                element.id = tinyMCE.DOM.uniqueId();
            }

            // create each tinyMCE instance synchronously. This addresses an issue when working with foreach bindings
            init_queue_next = init_queue_next.pipe(function () {
                var defer = $.Deferred();
                var init_options = $.extend({}, options, {
                    mode: 'none',
                    init_instance_callback: function (instance) {
                        instances_by_id[element.id] = instance;
                        ko.bindingHandlers.tinymce.update.apply(undefined, init_arguments);
                        defer.resolve(element.id);
                        if (options.hasOwnProperty("init_instance_callback")) {
                            options.init_instance_callback(instance);
                        }
                    }
                });
                setTimeout(function () {
                    tinyMCE.init(init_options);
                    setTimeout(function () {
                        tinyMCE.execCommand("mceAddControl", true, element.id);
                    }, 10);
                }, 10);
                return defer.promise();
            });
            el.val(value);
        },
        update: function (element, valueAccessor, allBindingsAccessor, context) {
            var el = $(element);
            var value = ko.utils.unwrapObservable(valueAccessor());
            var id = el.attr('id');

            //handle programmatic updates to the observable
            // also makes sure it doesn't update it if it's the same.
            // otherwise, it will reload the instance, causing the cursor to jump.
            if (id !== undefined && id !== '' && instances_by_id.hasOwnProperty(id)) {
                var content = instances_by_id[id].getContent({ format: 'raw' });
                if (content !== value) {
                    el.val(value);
                }
            }
        }
    };
}(jQuery));

ko.bindingHandlers.checkbox = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).checkbox();
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).checkbox("refresh");
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).datepicker();
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).datepicker("refresh");
    }
};


ko.bindingHandlers.expandingTextarea = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).expandingTextarea();
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).expandingTextarea("resize");
    }
};

ko.bindingHandlers.animate = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        if (valueAccessor()) {
            $(element).show();
        } else {
            $(element).hide();
        }
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        if (valueAccessor()) {
            $(element).slideDown(500, "linear");
        } else {
            $(element).slideUp(500, "linear");
        }
    }
};

ko.bindingHandlers.fadeOut = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();         
        var valueUnwrapped = ko.utils.unwrapObservable(value);
        var delay = valueUnwrapped.delay ? valueUnwrapped.delay : 0;
        var fadeTime = valueUnwrapped.fadeTime ? valueUnwrapped.fadeTime : 500;

        $(element).delay(delay).fadeOut(fadeTime, function () {
            if (valueUnwrapped.flag) {
                valueUnwrapped.flag(false);
            }
        });
    }
};

//function oninit() {

//    // Disable caching of AJAX responses on localhost

//    // create new MasterViewModel, and bind using knockout
//    var masterModel = new MasterViewModel();
//    $.connection.hub.start(function () {
//        masterModel.oninit();
//    });
//}


//$(oninit);

