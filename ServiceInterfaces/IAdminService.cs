using System;
using Entities;

namespace ServiceInterfaces
{
    public interface IAdminService
    {
        Post AddBlogPost(Post entry);
        void DeleteBlogPost(Guid Id);
    }
}