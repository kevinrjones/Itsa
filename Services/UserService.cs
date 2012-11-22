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
            if (GetRegisteredUser() != null)
            {
                throw new ItsaException("User has already registered");
            }
            _userRepository.Create(new User(userName, email, password));
        }

        public bool UnRegister()
        {
            if (GetRegisteredUser() == null)
            {
                return false;
            }
            _userRepository.Create(new User("", "", ""));
            return true;
        }

        public bool Logon(string userName, string password)
        {
            User user = GetRegisteredUser();
            if (user == null)
            {
                throw new ItsaException("User not yet registered");
            }
            return user.Name == userName && user.MatchPassword(password);
        }

        public User GetRegisteredUser()
        {
            var user = _userRepository.GetUser();
            if (user == null || string.IsNullOrEmpty(user.Name))
            {
                return null;
            }
            return user;
        }
    }
}
