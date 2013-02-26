var admin = function () {


    function init() {
        var page;
        $.connection.userHub.server.getUser()
            .done(function (data) {
                if (data) {
                    page = new PageViewModel(data);
                } else {
                    page = new PageViewModel({ isAuthenticated: false });
                }
            })
            .fail(function (error) {
                console.log(error);
                page = new PageViewModel({ isAuthenticated: false });
            }).always(function() {
                page.getPosts();
                ko.applyBindings(page);
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
    });
});
