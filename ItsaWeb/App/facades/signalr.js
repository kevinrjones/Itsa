define(function () {

    var deleteBlogPost = function (id) {
        return $.connection.adminHub.server.deleteBlogPost(id);
    };

    var getBlogEntries = function() {
        return $.connection.blogHub.server.getBlogEntries();
    };

    var isAuthenticated = function () {
        return $.connection.sessionHub.server.isAuthenticatedUser();
    };


    return {
        deleteBlogPost: deleteBlogPost,
        getBlogEntries: getBlogEntries,
        isAuthenticated: isAuthenticated
    };
});
