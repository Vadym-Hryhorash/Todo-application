using TodoApp.Models;

namespace TodoApp.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<Users> GetAllUsers();
        Users? GetUserById(int id);
        Users? GetUserByEmail(string email);
        Users CreateUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(int id);
    }
}
