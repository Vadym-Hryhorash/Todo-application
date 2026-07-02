using TodoApp.DataAccess.Repositories;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _userRepository;

        public UserService(IRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public Users? GetUserById(int id)
        {
            return _userRepository.FindByCondition(u => u.Id == id).FirstOrDefault();
        }

        public Users? GetUserByEmail(string email)
        {
            return _userRepository.FindByCondition(u => u.Email == email).FirstOrDefault();
        }

        public Users CreateUser(Users user)
        {
            var existingUser = GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new ArgumentException($"User with {user.Email} already exists.");
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return user;
        }

        public void UpdateUser(Users user)
        {
            _userRepository.Update(user);
            _userRepository.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _userRepository.Delete(user);
                _userRepository.SaveChanges();
            }
        }
    }
}
