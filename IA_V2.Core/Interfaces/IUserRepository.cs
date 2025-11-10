using System;
using IA_V2.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllUserByUserAsync(int idUser);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersWithTextsAsync();
    }
}
