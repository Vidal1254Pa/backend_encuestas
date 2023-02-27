using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encuestadora.Backend.Domain.Configuracion.Interfaces
{
	public interface ICategoriaRepository
	{
        Task<Pagination<Categoria>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<IEnumerable<Categoria>> List();
        Task<Categoria> Save(Categoria categoria);
        Task<Categoria> Update(Categoria categoria);
        Task Delete(int id);
        Task<Categoria> FindById(int id);
    }
}

