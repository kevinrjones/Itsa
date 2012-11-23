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

        public void AddBlogEntry(Post entry)
        {
            _repository.Create(entry);
        }
    }
}