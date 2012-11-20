using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Exceptions;
using ItsaRepository.Interfaces;
using ServiceInterfaces;

namespace Services
{
    public class SessionService : ISessionService
    {
        private readonly IUserRepository _userRepository;

        public SessionService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(string userName, string password, string email)
        {
            // todo: if user exist, throw exception
            if (GetUser() == null)
            {
                throw new ItsaException("User has already registered");
            }
            _userRepository.Create(new User(userName, password, email));
        }

        public bool Logon(string userName, string password)
        {
            User user = GetUser();
            if (user == null)
            {
                throw new ItsaException("User not yet registered");
            }
            return user.Name == userName && user.MatchPassword(password);
        }

        public User GetUser()
        {
            return _userRepository.GetUser();
        }
    }
}
