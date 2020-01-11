using System.Threading.Tasks;
using System.Collections.Generic;

using UdemyDatingApp.API.Models;

namespace UdemyDatingApp.API.Data
{
    public interface IDatingRepository
    {
        // create a method with a generic type (T for user or photo) wher T is a type of class
         void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
    }
}