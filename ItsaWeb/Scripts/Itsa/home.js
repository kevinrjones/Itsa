var home = function () {


    function init() {
        $.connection.hub.start(function () {
            $.connection.blogHub.getBlogEntries()
                .done(function (data) {
                    // iterate over the data array and display it
                    // ko.applyBindings(model);
                })
                .fail(function (error) {
                    console.log(error);
                });
        });
    }

    /* publicly-exposed methods */
    return {
        init: init
    };
}();

$(function() {
    home.init();
});

