define(['services/logger', 'i18n!nls/site', 'facades/signalr'], function (logger, resources, server) {
    var converter = Markdown.getSanitizingConverter();
    // ReSharper disable InconsistentNaming
    function BlogPost(params) {
        var self = this;
        self.id = ko.observable();
        self.postTitle = ko.observable();
        self.postBody = ko.observable();
        self.postCreated = ko.observable();
        self.postUpdated = ko.observable();
        self.Tags = ko.observable();
        self.isDraft = ko.observable(false);

        self.postBodyOutput = ko.computed(function () {
            var input = self.postBody();
            if (input) {
                return converter.makeHtml(input);
            } else {
                return "";
            }
        }, self);

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
            self.isDraft(item.IsDraft);
        } else {
            throw "No data for blog post";
        }

        self.editPost = function () {
            var router = require('durandal/plugins/router');
            router.navigateTo('#edit/' + self.id());
        };

        self.draftStatus = function () {
            self.isDraft(!self.isDraft());
            return true;
        };

        self.deletePost = function () {
            var app = require('durandal/app');

            app.showMessage(resources.deleteThisPost, resources.delete, ['No', 'Yes']).then(function (result) {
                if (result === "Yes") {
                    server.deleteBlogPost(self.id())
                        .done(function () {
                            self.parent.remove(self);
                        })
                        .fail(function () {
                        });
                }
            });
        };

    };

    return BlogPost;

});

// ReSharper restore InconsistentNaming
