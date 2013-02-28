/* Replacement for JS confirm() */
function Confirm(args) {
    var defaults = { message: "Click okay",body:"", buttons: [{ text: "OK", value: true, cssclass: "ok" }, { text: "Cancel", value: false, cssclass: "cancel" }], height: 200 };
    var values = {};
    $.extend(values, defaults, args);
    Alert(values);
};

/* Replacement for JS alert() */
function Alert(args) {
    var defaults = { message: "Click okay", body: "Text goes here", buttons: [{text: "OK", value: true, cssclass: "ok"}], height: 200 };
    var values = {};
    $.extend(values, defaults, args);

    $("<div>").attr("id", "backgroundCover").addClass("ui-widget-overlay").appendTo("body");
    $("<div>").addClass("alert").css({ height: values.height + "px" }).appendTo("body");
    $("<div>" + values.message + "</div>").addClass("heading").appendTo(".alert");
    if (values.body) {
        $("<div>" + values.body + "</div>").addClass("content").appendTo(".alert");
    }
    $("<div>").addClass("buttons").appendTo(".alert");
    for (var i = 0; i < values.buttons.length; i++) {
        $("<button>" + values.buttons[i].text + "</button>").addClass(values.buttons[i].cssclass).appendTo(".buttons");
    }
    var winHeight = $(window).height();
    var elHeight = $(".alert").height();

    $(".alert").css("top", (winHeight - elHeight) / 2);

    $(".alert").find("button").click(function () {
        $(".alert").add(".ui-widget-overlay").remove();
    });

    var createfunction = function(text, value, callback) {
        return function() {
            console.log("alert button " + text + " returns " + value);
            callback(value);
        };
    };

    for (var j = 0; j < values.buttons.length; j++) {
        if (values.callback) {
            $(".alert").find("button." + values.buttons[j].cssclass).click(createfunction(values.buttons[j].text, values.buttons[j].value, values.callback));
        }
    }
}

function Block(message) {
    if (!$.blockUI.setDefaults) {
        $.blockUI.setDefaults = true;
        $.blockUI.defaults.css = {
            padding: 0,
            margin: 0,
            width: '30%',
            top: '40%',
            left: '35%',
            textAlign: 'center',
            color: '#404040',
            border: 'none',
            backgroundColor: 'transparent',
            cursor: 'wait'
        };
        $.blockUI.defaults.overlayCSS = {
            backgroundColor: '#0f0f0f',
            opacity: 0.5,
        cursor: 'wait'
        };
    }
    $.blockUI({ message: '<h1><img src="/Content/icons/grey_loading.gif" />&nbsp;' + message + '</h1>' });
}

function Unblock() {
    $.unblockUI();
}
