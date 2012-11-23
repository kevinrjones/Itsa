using System;
using System.Collections.Generic;
using Entities;
using Repository;

namespace ItsaRepository.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IList<Post> GetPosts();
        Post GetBlogPost(Guid id);
        IList<Post> GetBlogPosts(int year, int month, int day, string link);
        Post AddComment(Guid id, string name, string comment);
        void Update(Guid postId, string title, string entry);
        void Delete(Guid postId);
        int GetCountOfPostsForBlog();
    }
}
