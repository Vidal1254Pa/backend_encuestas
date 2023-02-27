using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encuestadora.Backend.Infraestructure
{
    public interface ICustomConnection
    {
        Task<IDbConnection> BeginConnection();
        Task CloseConnection();
    }
}
