using Entities;
using ItsaRepository.Interfaces;
using ServiceInterfaces;

namespace Services
{
    public class AdminService : IAdminService
    {
        private readonly IBlogRepository _repository;

        public AdminService(IBlogRepository repository)
        {
            _repository = repository;
        }

        public void AddBlogEntry(BlogEntry entry)
        {
            _repository.Create(entry);
        }
    }
}