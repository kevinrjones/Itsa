define(['services/logger', 'i18n!nls/site', 'facades/signalr'], function (logger, resources, server) {

    // ReSharper disable InconsistentNaming
    function BlogPost(params) {
        var self = this;
        self.id = ko.observable();
        self.postTitle = ko.observable();
        self.postBody = ko.observable();
        self.postCreated = ko.observable();
        self.postUpdated = ko.observable();

        if (params && params.parent) {
            self.parent = params.parent;
        } else {
            throw "Params not set";
        }

        if (params && params.item) {
            var item = params.item;
            self.postTitle(item.Title);
            self.postBody(item.Body);
            self.postCreated(new Date(item.EntryAddedDate).toString("d-MMM-yyyy HH:MM"));
            self.postUpdated(new Date(item.EntryUpdatedDate).toString("d-MMM-yyyy HH:MM"));
            self.id(item.Id);
        } else {
            throw "No data for blog post";
        }

        self.editPost = function () {
            console.log("edit");
        };

        self.deletePost = function () {
            var app = require('durandal/app');

            app.showMessage(resources.deleteThisPost, resources.delete, ['No', 'Yes']).then(function (result) {
                if (result === "Yes") {
                    server.deleteBlogPost(self.id())
                        .done(function (data) {
                            self.parent.remove(self);
                        })
                        .fail(function (error) {
                        });
                }
            });
        };

    };

    return BlogPost;

});

// ReSharper restore InconsistentNaming
