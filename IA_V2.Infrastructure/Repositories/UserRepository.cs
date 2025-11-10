using IA_V2.Core.Entities;
using IA_V2.Core.Enum;
using IA_V2.Core.Interfaces;
using IA_V2.Infrastructure.Data;
using IA_V2.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IDapperContext _dapper;
        //private readonly SocialMediaContext _context;
        public UserRepository(InteligenciaArtificialV2Context context, IDapperContext dapper) : base(context)
        {
            _dapper = dapper;
            //_context = context;
        }


        public async Task<IEnumerable<User>> GetAllUsersDapperAsync(int limit = 10)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => UserQueries.UserQuerySqlServer,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<User>(sql, new { Limit = limit });
            }
            catch (Exception err)
            {
                throw new Exception($"Error al obtener usuarios con Dapper: {err.Message}", err);
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dapper.QueryFirstOrDefaultAsync<User>(
                UserQueries.GetUserByEmail,
                new { Email = email });
        }

        public async Task<IEnumerable<User>> GetUsersWithTextsAsync()
        {
            return await _dapper.QueryAsync<User>(UserQueries.GetUsersWithTexts);
        }

        public async Task<IEnumerable<User>> GetUsersPagedAsync(int pageNumber, int pageSize)
        {
            var offset = (pageNumber - 1) * pageSize;
            return await _dapper.QueryAsync<User>(
                UserQueries.GetUsersPaged,
                new { Offset = offset, PageSize = pageSize });
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _dapper.ExecuteScalarAsync<int>(UserQueries.GetUsersCount);
        }

        public Task<IEnumerable<User>> GetAllUserByUserAsync(int idUser)
        {
            throw new NotImplementedException();
        }
    }
}
