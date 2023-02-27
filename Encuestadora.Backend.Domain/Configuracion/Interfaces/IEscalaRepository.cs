using System;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Domain.Configuracion.Interfaces
{
	public interface IEscalaRepository
	{
        Task<Pagination<Escala>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<IEnumerable<Escala>> List();
        Task<Escala> Save(Escala escala);
        Task<Escala> Update(Escala escala);
        Task Delete(int id);
        Task<Escala> FindById(int id);
    }
}

