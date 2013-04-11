define(function () {

    var deleteBlogPost = function (id) {
        return $.connection.adminHub.server.deleteBlogPost(id);
    };

    var getBlogPost = function (id) {
        return $.connection.blogHub.server.get(id);
    };

    var getAllBlogEntries = function () {
        return $.connection.blogHub.server.list(true);
    };

    var getBlogEntries = function () {
        return $.connection.blogHub.server.list(false);
    };

    var isAuthenticated = function () {
        return $.connection.sessionHub.server.isAuthenticatedUser();
    };

    var createPost = function (post) {
        return $.connection.blogHub.server.create(post);
    };

    var updatePost = function (post) {
        return $.connection.blogHub.server.update(post);
    };

    return {
        deleteBlogPost: deleteBlogPost,
        getBlogEntries: getBlogEntries,
        getAllBlogEntries: getAllBlogEntries,
        isAuthenticated: isAuthenticated,
        createPost: createPost,
        updatePost: updatePost,
        getBlogPost: getBlogPost
    };
});
