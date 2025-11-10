using IA_V2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<IEnumerable<User>> GetAllUsersDapperAsync(int limit = 10);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserDapperAsync(int id);
        Task InsertUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
