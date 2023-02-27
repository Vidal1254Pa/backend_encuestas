using System;
using System.Data;
using System.Data.Common;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
namespace Encuestadora.Backend.Infraestructure.Configuracion
{
	public class EscalaRepository:IEscalaRepository
	{
        protected readonly ICustomConnection _connection;
        public EscalaRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task Delete(int id)
        {
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    var items = await scope.ExecuteAsync("USP_Eliminar_Escala",
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

        public async Task<Escala> FindById(int id)
        {
            Escala entity = null;
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    entity = await scope.QueryFirstOrDefaultAsync<Escala>("USP_Buscar_Escala_x_Id",
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

        public async Task<IEnumerable<Escala>> List()
        {
            IEnumerable<Escala> lista;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    lista = await scope.QueryAsync<Escala>("USP_Listar_Escala", commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return lista;
        }

        public async Task<Pagination<Escala>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<Escala>? paginacion = null;
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
                    paginacion = new Pagination<Escala>();
                    paginacion.Records = await scope.QueryAsync<Escala>("USP_Paginate_Escala", dinamycParams, commandType: CommandType.StoredProcedure);
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

        public async Task<Escala> Save(Escala escala)
        {
            Escala? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Escala>("USP_Registrar_Escala",
                    new
                    {

                        @Detalle=escala.Detalle
                    }, commandType: CommandType.StoredProcedure);
                    entity = escala;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<Escala> Update(Escala escala)
        {
            Escala? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Escala>("USP_Actualizar_Escala",
                    new
                    {
                        @Id = escala.Id,
                        @Detalle = escala.Detalle
                    }, commandType: CommandType.StoredProcedure);
                    entity = escala;
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

