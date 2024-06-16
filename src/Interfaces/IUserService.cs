using OnlineBookstoreMS.Models.Entity;

namespace OnlineBookstoreMS.Interface
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Register(User user, string password);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAll();
        Task Update(User user, string password = null);
        Task Delete(int id);
    }

}
