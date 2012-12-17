using System;
using Entities;

namespace ServiceInterfaces
{
    public interface IPostService
    {
        Post GetPost(Guid postId);
        void UpdatePost(Guid postId, string title, string content);
        void DeletePost(Guid postId);
        void CreatePost(Post post);
        int GetCountOfPostsForBlog();

    }
}
