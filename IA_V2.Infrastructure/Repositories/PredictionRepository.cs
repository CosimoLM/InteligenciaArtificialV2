using IA_V2.Core.Entities;
using IA_V2.Core.Enum;
using IA_V2.Core.Interfaces;
using IA_V2.Infrastructure.Data;
using IA_V2.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IA_V2.Infrastructure.Repositories
{
    public class PredictionRepository : BaseRepository<Prediction>, IPredictionRepository
    {
        private readonly IDapperContext _dapper;

        public PredictionRepository(InteligenciaArtificialV2Context context, IDapperContext dapper)
            : base(context)
        {
            _dapper = dapper;
        }
        public async Task<IEnumerable<Prediction>> GetAllPredictionsDapperAsync(int limit = 10)
        {
            try
            {
                var sql = _dapper.Provider switch
                {
                    DatabaseProvider.SqlServer => PredictionQueries.PredictionQuerySqlServer,
                    _ => throw new NotSupportedException("Provider no soportado")
                };

                return await _dapper.QueryAsync<Prediction>(sql, new { Limit = limit });
            }
            catch (Exception err)
            {
                throw new Exception($"Error al obtener predicciones con Dapper: {err.Message}", err);
            }
        }
    }
}
