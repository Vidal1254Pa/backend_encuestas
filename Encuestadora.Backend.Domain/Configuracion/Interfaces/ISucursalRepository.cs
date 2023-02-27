using System;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Domain.Configuracion.Interfaces
{
	public interface ISucursalRepository
	{
        Task<Pagination<Sucursal>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<IEnumerable<Sucursal>> List();
        Task<Sucursal> Save(Sucursal sucursal);
        Task<Sucursal> Update(Sucursal sucursal);
        Task Delete(int id);
        Task<Sucursal> FindById(int id);
    }
}

