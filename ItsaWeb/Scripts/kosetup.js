ko.bindingHandlers.tinymce = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).tinymce({
            // Location of TinyMCE script
            script_url: 'scripts/tiny_mce.js',

            // General options
            theme: "advanced",
            plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

            // Theme options
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,code,|,preview",
            theme_advanced_buttons3: "",
            theme_advanced_buttons4: "",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,

            // Example content CSS (should be your site CSS)
            //content_css : "css/content.css",

            // Drop lists for link/image/media/template dialogs
            //template_external_list_url : "lists/template_list.js",
            //external_link_list_url : "lists/link_list.js",
            //external_image_list_url : "lists/image_list.js",
            //media_external_list_url : "lists/media_list.js",

            // Replace values for the template plugin
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        $(element).tinymce({
            // Location of TinyMCE script
            script_url: 'scripts/tiny_mce.js',

            // General options
            theme: "advanced",
            plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",

            // Theme options
            theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,code,|,preview",
            theme_advanced_buttons3: "",
            theme_advanced_buttons4: "",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,

            // Example content CSS (should be your site CSS)
            //content_css : "css/content.css",

            // Drop lists for link/image/media/template dialogs
            //template_external_list_url : "lists/template_list.js",
            //external_link_list_url : "lists/link_list.js",
            //external_image_list_url : "lists/image_list.js",
            //media_external_list_url : "lists/media_list.js",

            // Replace values for the template plugin
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }
        });
    }
};



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

