using System;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Domain.Configuracion.Interfaces
{
	public interface IProvinciaRepository
	{
        Task<Pagination<Provincia>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<IEnumerable<Provincia>> List();
        Task<Provincia> Save(Provincia provincia);
        Task<Provincia> Update(Provincia provincia);
        Task Delete(int id);
        Task<Provincia> FindById(int id);
    }
}

