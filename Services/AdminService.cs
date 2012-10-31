using Entities;
using ItsaRepository.interfaces;
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