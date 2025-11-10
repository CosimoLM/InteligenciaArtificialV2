using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Infrastructure.Queries
{
    public static class UserQueries
    {
        public static string UserQuerySqlServer = @"
            SELECT Id, Name, Email, Password 
            FROM Users 
            ORDER BY Id DESC
            OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY;";

        public static string GetAllUsers = @"
            SELECT Id, Name, Email, Password 
            FROM Users 
            ORDER BY Id DESC";

        public static string GetUserById = @"
            SELECT Id, Name, Email, Password 
            FROM Users 
            WHERE Id = @Id";

        public static string GetUserByEmail = @"
            SELECT Id, Name, Email, Password 
            FROM Users 
            WHERE Email = @Email";

        public static string GetUsersPaged = @"
            SELECT Id, Name, Email, Password 
            FROM Users 
            ORDER BY Id DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        public static string GetUsersCount = "SELECT COUNT(*) FROM Users";

        public static string GetUsersWithTextsCount = @"
            SELECT COUNT(DISTINCT u.Id) 
            FROM Users u
            INNER JOIN Texts t ON u.Id = t.UserId";

        public static string GetUsersWithTexts = @"
            SELECT u.*, COUNT(t.Id) as TextsCount
            FROM Users u
            LEFT JOIN Texts t ON u.Id = t.UserId
            GROUP BY u.Id, u.Name, u.Email, u.Password
            HAVING COUNT(t.Id) > 0
            ORDER BY TextsCount DESC";
    }
}
