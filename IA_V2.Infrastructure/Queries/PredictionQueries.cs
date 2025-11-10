using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Infrastructure.Queries
{
    public static class PredictionQueries
    {
        public static string PredictionQuerySqlServer = @"
            SELECT p.Id, p.TextId, p.UserId, p.Result, p.Probability, p.Date
            FROM Predictions p
            ORDER BY p.Date DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";
        public static string GetAllPredictions = @"
            SELECT p.Id, p.TextId, p.UserId, p.Result, p.Probability, p.Date,
                   t.Content as TextContent, u.Name as UserName
            FROM Predictions p
            LEFT JOIN Texts t ON p.TextId = t.Id
            LEFT JOIN Users u ON p.UserId = u.Id
            ORDER BY p.Date DESC";

        public static string GetPredictionById = @"
            SELECT p.Id, p.TextId, p.UserId, p.Result, p.Probability, p.Date,
                   t.Content as TextContent, u.Name as UserName
            FROM Predictions p
            LEFT JOIN Texts t ON p.TextId = t.Id
            LEFT JOIN Users u ON p.UserId = u.Id
            WHERE p.Id = @Id";

        public static string GetPredictionsPaged = @"
            SELECT p.Id, p.TextId, p.UserId, p.Result, p.Probability, p.Date,
                   t.Content as TextContent, u.Name as UserName
            FROM Predictions p
            LEFT JOIN Texts t ON p.TextId = t.Id
            LEFT JOIN Users u ON p.UserId = u.Id
            ORDER BY p.Date DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        public static string GetPredictionsCount = "SELECT COUNT(*) FROM Predictions";

        public static string GetPredictionsByUser = @"
            SELECT p.Id, p.TextId, p.UserId, p.Result, p.Probability, p.Date,
                   t.Content as TextContent
            FROM Predictions p
            LEFT JOIN Texts t ON p.TextId = t.Id
            WHERE p.UserId = @UserId
            ORDER BY p.Date DESC";

        public static string GetPredictionsByText = @"
            SELECT p.Id, p.TextId, p.UserId, p.Result, p.Probability, p.Date,
                   u.Name as UserName
            FROM Predictions p
            LEFT JOIN Users u ON p.UserId = u.Id
            WHERE p.TextId = @TextId
            ORDER BY p.Probability DESC";

        public static string GetPredictionStats = @"
            SELECT 
                Result as Category,
                COUNT(*) as Count,
                AVG(Probability) as AverageProbability,
                MAX(Probability) as MaxProbability,
                MIN(Probability) as MinProbability
            FROM Predictions
            GROUP BY Result
            ORDER BY Count DESC";
    }
}
