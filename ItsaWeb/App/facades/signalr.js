define(function () {

    var deleteBlogPost = function (id) {
        return $.connection.adminHub.server.deleteBlogPost(id);
    };

    var getBlogEntries = function() {
        return $.connection.blogHub.server.list();
    };

    var isAuthenticated = function () {
        return $.connection.sessionHub.server.isAuthenticatedUser();
    };

    var createPost = function(post) {
        return $.connection.blogHub.server.create(post);
    };

    return {
        deleteBlogPost: deleteBlogPost,
        getBlogEntries: getBlogEntries,
        isAuthenticated: isAuthenticated,
        createPost: createPost
    };
});
