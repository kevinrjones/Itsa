define(['facades/signalr', 'sinon'], function (server, sinon) {
    var spy;
    module("blogPost callback Tests", {
        setup: function () {
            spy = sinon.stub();
            $.connection = {
                adminHub: {
                    server: {
                        deleteBlogPost: spy
                    }
                },
                blogHub: {
                    server: {
                        list: spy
                    }
                }
            };
        },
        teardown: function () {
        }
    });

    test("deleteBlogPost passes correct id", function () {
        spy.returns(new jQuery.Deferred());
        server.deleteBlogPost(1);
        ok(spy.called, "deletePost should be called");
        ok(spy.calledWith(1), "deletePost should be called with correct id");
    });

    test("getBlogEntries is called", function () {
        spy.returns(new jQuery.Deferred());
        server.getBlogEntries();
        ok(spy.called, "getBlogEntries should be called");
    });

});
