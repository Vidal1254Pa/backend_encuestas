using System;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Domain.Configuracion.Interfaces
{
	public interface ICiudadRepository
	{
        Task<Pagination<Ciudad>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<IEnumerable<Ciudad>> List();
        Task<Ciudad> Save(Ciudad ciudad);
        Task<Ciudad> Update(Ciudad ciudad);
        Task Delete(int id);
        Task<Ciudad> FindById(int id);
    }
}

