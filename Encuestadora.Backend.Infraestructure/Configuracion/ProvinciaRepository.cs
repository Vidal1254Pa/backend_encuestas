using System;
using System.Data;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
namespace Encuestadora.Backend.Infraestructure.Configuracion
{
	public class ProvinciaRepository:IProvinciaRepository
	{
        protected readonly ICustomConnection _connection;
        public ProvinciaRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task Delete(int id)
        {
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    var items = await scope.ExecuteAsync("USP_Eliminar_Provincia",
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

        public async Task<Provincia> FindById(int id)
        {
            Provincia entity = null;
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    entity = await scope.QueryFirstOrDefaultAsync<Provincia>("USP_Buscar_Provincia_x_Id",
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

        public async Task<IEnumerable<Provincia>> List()
        {
            IEnumerable<Provincia> lista;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    lista = await scope.QueryAsync<Provincia>("USP_Listar_Provincia", commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return lista;
        }

        public async Task<Pagination<Provincia>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<Provincia>? paginacion = null;
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
                    paginacion = new Pagination<Provincia>();
                    paginacion.Records = await scope.QueryAsync<Provincia>("USP_Paginate_Provincia", dinamycParams, commandType: CommandType.StoredProcedure);
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

        public async Task<Provincia> Save(Provincia provincia)
        {
            Provincia? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Provincia>("USP_Registrar_Provincia",
                    new
                    {

                        @Nombre=provincia.Nombre
                    }, commandType: CommandType.StoredProcedure);
                    entity = provincia;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<Provincia> Update(Provincia provincia)
        {
            Provincia? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Provincia>("USP_Actualizar_Provincia",
                    new
                    {
                        @Id=provincia.Id,
                        @Nombre=provincia.Nombre
                    }, commandType: CommandType.StoredProcedure);
                    entity = provincia;
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

