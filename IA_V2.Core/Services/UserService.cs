using IA_V2.Core.Entities;
using IA_V2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.Services
{
    public class UserService : IUserService
    {
        public readonly IBaseRepository<User> _userRepository;

        public UserService(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task InsertUserAsync(User user)
        {

            await _userRepository.Add(user);
        }

        public async Task UpdateUserAsync(User post)
        {
            await _userRepository.Update(post);
        }
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.Delete(id);
        }
    }
}
