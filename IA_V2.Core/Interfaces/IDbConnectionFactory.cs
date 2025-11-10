using IA_V2.Core.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.Interfaces
{
    public interface IDbConnectionFactory
    {
        DatabaseProvider Provider { get; }
        IDbConnection CreateConnection();
    }
}
