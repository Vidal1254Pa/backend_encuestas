using System;
using System.Data;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
namespace Encuestadora.Backend.Infraestructure.Configuracion
{
	public class CiudadRepository:ICiudadRepository
	{
        protected readonly ICustomConnection _connection;
        public CiudadRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task Delete(int id)
        {
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    var items = await scope.ExecuteAsync("USP_Eliminar_Ciudad",
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

        public async Task<Ciudad> FindById(int id)
        {
            Ciudad entity = null;
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    entity = await scope.QueryFirstOrDefaultAsync<Ciudad>("USP_Buscar_Ciudad_x_Id",
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

        public async Task<IEnumerable<Ciudad>> List()
        {
            IEnumerable<Ciudad> lista;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    lista = await scope.QueryAsync<Ciudad>("USP_Listar_Ciudad", commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return lista;
        }

        public async Task<Pagination<Ciudad>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<Ciudad>? paginacion = null;
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
                    paginacion = new Pagination<Ciudad>();
                    paginacion.Records = await scope.QueryAsync<Ciudad>("USP_Paginate_Ciudad", dinamycParams, commandType: CommandType.StoredProcedure);
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

        public async Task<Ciudad> Save(Ciudad ciudad)
        {
            Ciudad? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Ciudad>("USP_Registrar_Ciudad",
                    new
                    {

                        @Nombre=ciudad.Nombre,
                        @Provincia_Id=ciudad.Provincia_Id
                    }, commandType: CommandType.StoredProcedure);
                    entity = ciudad;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<Ciudad> Update(Ciudad ciudad)
        {
            Ciudad? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Ciudad>("USP_Actualizar_Ciudad",
                    new
                    {
                        @Id = ciudad.Id,
                        @Nombre = ciudad.Nombre,
                        @Provincia_Id = ciudad.Provincia_Id
                    }, commandType: CommandType.StoredProcedure);
                    entity = ciudad;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }
    }
}

