using System;
using System.Data.SqlClient;
using Encuestadora.Backend.Domain.Requerimiento.Domain;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Domain.Requerimiento.Interfaces
{
	public interface IEncuestaRequerimientoRepository
	{
        Task<Pagination<EncuestaRequerimiento>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir);
        Task<EncuestaRequerimiento> Save(EncuestaRequerimiento encuestaRequerimiento, SqlConnection conexion, SqlTransaction transaccion);
        Task<int> Save_Detalle(DetalleEncuesta detalleEncuesta, SqlConnection conexion, SqlTransaction transaccion);
        Task<EncuestaRequerimiento> FindById(int id);

    }
}

