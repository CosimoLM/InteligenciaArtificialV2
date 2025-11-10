using IA_V2.Core.Entities;
using IA_V2.Core.Exceptions;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperContext _dapper;

        public UserService(IUnitOfWork unitOfWork, IDapperContext dapper)
        {
            _unitOfWork = unitOfWork;
            _dapper = dapper;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _unitOfWork.UserRepository.GetAll();
        }

        public async Task<IEnumerable<User>> GetAllUsersDapperAsync(int limit = 10)
        {
            var sql = "SELECT * FROM Users ORDER BY Id DESC LIMIT @Limit";
            return await _dapper.QueryAsync<User>(sql, new { Limit = limit });
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetById(id);
        }

        public async Task<User> GetUserDapperAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await _dapper.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }
        public async Task InsertUserAsync(User user)
        {
            // Validar email único
            var existingUser = await _unitOfWork.UserRepository.GetAll();
            foreach (var u in existingUser)
            {
                if (u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
                    throw new BusinessException("El email ya está registrado");
            }

            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await GetUserAsync(user.Id);

            // Validar email único (excluyendo el usuario actual)
            var allUsers = await _unitOfWork.UserRepository.GetAll();
            foreach (var u in allUsers)
            {
                if (u.Id != user.Id && u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
                    throw new BusinessException("El email ya está registrado");
            }

            await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserAsync(id);

            // Validar que no tenga textos asociados
            var userTexts = await _unitOfWork.TextRepository.GetAll();
            // Lógica de validación

            await _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
