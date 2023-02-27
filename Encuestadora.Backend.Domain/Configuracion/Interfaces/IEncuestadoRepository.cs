using System;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Domain.Configuracion.Interfaces
{
	public interface IEncuestadoRepository
	{
        Task<Pagination<Encuestado>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<IEnumerable<Encuestado>> List();
        Task<Encuestado> Save(Encuestado encuestado);
        Task<Encuestado> Update(Encuestado encuestado);
        Task Delete(string id);
        Task<Encuestado> FindById(string id);
    }
}

