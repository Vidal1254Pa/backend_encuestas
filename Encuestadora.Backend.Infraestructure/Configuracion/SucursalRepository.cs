using System;
using System.Data;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
namespace Encuestadora.Backend.Infraestructure.Configuracion
{
	public class SucursalRepository:ISucursalRepository
	{
        protected readonly ICustomConnection _connection;
        public SucursalRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task Delete(int id)
        {
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    var items = await scope.ExecuteAsync("USP_Eliminar_Sucursal",
                    new
                    {
                        @Id = id,
                    }, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
        }

        public async Task<Sucursal> FindById(int id)
        {
            Sucursal entity = null;
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    entity = await scope.QueryFirstOrDefaultAsync<Sucursal>("USP_Buscar_Sucursal_x_Id",
                    new
                    {
                        @Id = id,
                    }, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<IEnumerable<Sucursal>> List()
        {
            IEnumerable<Sucursal> lista;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    lista = await scope.QueryAsync<Sucursal>("USP_Listar_Sucursal", commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return lista;
        }

        public async Task<Pagination<Sucursal>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<Sucursal>? paginacion = null;
            DynamicParameters dinamycParams = new();
            dinamycParams.Add("Page", page);
            dinamycParams.Add("Size", size);
            dinamycParams.Add("Search", search);
            dinamycParams.Add("OrderBy", orderBy);
            dinamycParams.Add("OrderDir", orderDir);
            dinamycParams.Add("TotalGlobal", null, DbType.Int32, ParameterDirection.Output);
            dinamycParams.Add("TotalFiltered", null, DbType.Int32, ParameterDirection.Output);
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    paginacion = new Pagination<Sucursal>();
                    paginacion.Records = await scope.QueryAsync<Sucursal>("USP_Paginate_Sucursal", dinamycParams, commandType: CommandType.StoredProcedure);
                    paginacion.TotalGlobal = dinamycParams.Get<int>("TotalGlobal");
                    paginacion.TotalFiltered = dinamycParams.Get<int>("TotalFiltered");
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return paginacion;
        }

        public async Task<Sucursal> Save(Sucursal sucursal)
        {
            Sucursal? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Sucursal>("USP_Registrar_Sucursal",
                    new
                    {
                        @Nombre=sucursal.Nombre,
                        @Ciudad_Id=sucursal.Ciudad_Id
                    }, commandType: CommandType.StoredProcedure);
                    entity = sucursal;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<Sucursal> Update(Sucursal sucursal)
        {
            Sucursal? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Sucursal>("USP_Actualizar_Sucursal",
                    new
                    {
                        @Id = sucursal.Id,
                        @Nombre = sucursal.Nombre,
                        @Ciudad_Id = sucursal.Ciudad_Id
                    }, commandType: CommandType.StoredProcedure);
                    entity = sucursal;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return sucursal;
        }
    }
}

