using System;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Domain.Configuracion.Interfaces
{
	public interface IEncuestaRepository
	{
        Task<Pagination<Encuesta>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<IEnumerable<Encuesta>> List();
        Task<Encuesta> Save(Encuesta encuesta);
        Task<Encuesta> Update(Encuesta encuesta);
        Task Delete(int id);
        Task<Encuesta> FindById(int id);
    }
}

