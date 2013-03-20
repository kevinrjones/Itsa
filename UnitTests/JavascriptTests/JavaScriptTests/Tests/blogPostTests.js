/// <reference path="../Scripts/jquery-1.4.4-vsdoc.js" />
/// <reference path="Scripts/qunit.js" />
define(['app/viewmodels/blogPost.js', 'res'], function (BlogPost, resources) {
    module("blogPost Tests", {
        setup: function () {
        }
    });

    test("initialize blogpost", function () {
        resources.setResource('ItsaWeb.Resources.Resources.DeletePost', 'Test Value');
        var post = new BlogPost();
        
        equal(post.deleteButtonTitle, 'Test Value');
    });
});
