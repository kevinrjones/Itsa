using System;
using System.Collections.Generic;
using Entities;

namespace ServiceInterfaces
{
    public interface IPostService
    {
        Post GetPost(Guid postId);
        void UpdatePost(Post post);
        void DeletePost(Guid postId);
        Post CreatePost(Post post);
        int GetCountOfPostsForBlog();
        IList<Post> GetAllPosts();
        IList<Post> GetPublishedPosts();
    }
}
