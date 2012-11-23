using System;
using Entities;

namespace ServiceInterfaces
{
    public interface IPostService
    {
        Post Get(Guid postId);
        void Update(Guid postId, string title, string content);
        void Delete(Guid postId);
        void CreatePost(Post post);
    }
}
