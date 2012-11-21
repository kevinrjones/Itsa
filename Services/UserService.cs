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

        public void Register(string userName, string password, string email)
        {
            var user = GetUser();
            if (!string.IsNullOrEmpty(user.Name))
            {
                throw new ItsaException("User has already registered");
            }
            _userRepository.Create(new User(userName, password, email));
        }

        public void UnRegister()
        {
            var user = GetUser();
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ItsaException("User has already unregistered");
            }
            _userRepository.Create(new User("", "", ""));
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
