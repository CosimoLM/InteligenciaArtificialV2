using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Infrastructure.Queries
{
    public static class TextQueries
    {

        public static string TextQuerySqlServer = @"
            SELECT Id, Content, FechaEnvio, UserId 
            FROM Texts 
            ORDER BY FechaEnvio DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string GetAllTexts = @"
            SELECT Id, Content, FechaEnvio, UserId 
            FROM Texts 
            ORDER BY FechaEnvio DESC";

        public static string GetTextById = @"
            SELECT Id, Content, FechaEnvio, UserId 
            FROM Texts 
            WHERE Id = @Id";

        public static string GetTextsPaged = @"
            SELECT Id, Content, FechaEnvio, UserId 
            FROM Texts 
            ORDER BY FechaEnvio DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        public static string GetTextsCount = "SELECT COUNT(*) FROM Texts";

        public static string GetTextsByUser = @"
            SELECT Id, Content, FechaEnvio, UserId 
            FROM Texts 
            WHERE UserId = @UserId 
            ORDER BY FechaEnvio DESC";

        public static string GetTextsWithPredictions = @"
            SELECT t.*, p.Result as PredictionResult, p.Probability as PredictionProbability
            FROM Texts t
            LEFT JOIN Predictions p ON t.Id = p.TextId
            WHERE p.Id IS NOT NULL
            ORDER BY t.FechaEnvio DESC";

        public static string GetRecentTexts = @"
            SELECT Id, Content, FechaEnvio, UserId 
            FROM Texts 
            WHERE FechaEnvio >= DATEADD(day, -30, GETDATE())
            ORDER BY FechaEnvio DESC";

        public static string GetTextsByContentSearch = @"
            SELECT Id, Content, FechaEnvio, UserId 
            FROM Texts 
            WHERE Content LIKE '%' + @SearchText + '%'
            ORDER BY FechaEnvio DESC";
    }
}
