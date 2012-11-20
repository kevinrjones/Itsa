using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repository;

namespace ItsaRepository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser();
    }
}
