using System;
using System.Linq;
using Entities;
using ItsaRepository.Interfaces;
using ServiceInterfaces;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly IPostRepository _repository;

        public AdminService(IPostRepository repository)
        {
            _repository = repository;
        }

        public Post AddBlogPost(Post entry)
        {
            return _repository.Create(entry);
        }

        public void DeleteBlogPost(Guid id)
        {
            var post = (from e in _repository.Entities
                        where e.Id == id
                        select e).FirstOrDefault();
            _repository.Delete(post);
        }
    }
}