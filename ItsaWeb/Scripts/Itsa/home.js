var home = function () {



    function init() {
        $.connection.blogHub.getBlogEntries()
            .done(function (data) {
                var model = new BlogPostsModel();
                ko.applyBindings(model, mainsection);
            })
            .fail(function (error) {
                console.log(error);
            });
    }

    /* publicly-exposed methods */
    return {
        init: init
    };
}();

$(function () {
    $.connection.hub.start(function () {
        admin.init();
        home.init();
    });
});

