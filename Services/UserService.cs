using System.Linq;
using Entities;
using Exceptions;
using ItsaRepository.Interfaces;
using ServiceInterfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public User Register(string email, string password)
        {
            if (GetRegisteredUser() != null)
            {
                throw new ItsaException("User has already registered");
            }
            return _userRepository.Create(new User(email, password));
        }

        public bool UnRegister()
        {
            if (GetRegisteredUser() == null)
            {
                return false;
            }
            _userRepository.Create(new User("", ""));
            return true;
        }

        public User Logon(string email, string password)
        {
            User user = GetRegisteredUser();
            if (user == null || user.Email != email || !user.MatchPassword(password))
            {
                throw new ItsaException("Invalid credentials");
            }
            return user;
        }

        public User GetRegisteredUser()
        {
            var user = GetUser();
            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                return null;
            }
            return user;
        }

        public User GetUser()
        {
            return _userRepository.Entities.FirstOrDefault();
        }

    }
}
