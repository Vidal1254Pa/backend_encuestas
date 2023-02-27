using System;
using System.Data;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
namespace Encuestadora.Backend.Infraestructure.Configuracion
{
	public class EncuestadoRepository:IEncuestadoRepository
	{
        protected readonly ICustomConnection _connection;
        public EncuestadoRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task Delete(string id)
        {
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    var items = await scope.ExecuteAsync("USP_Eliminar_Encuestado",
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

        public async Task<Encuestado> FindById(string id)
        {
            Encuestado entity = null;
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    entity = await scope.QueryFirstOrDefaultAsync<Encuestado>("USP_Buscar_Encuestado_x_Id",
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

        public async Task<IEnumerable<Encuestado>> List()
        {
            IEnumerable<Encuestado> lista;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    lista = await scope.QueryAsync<Encuestado>("USP_Listar_Encuestado", commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return lista;
        }

        public async Task<Pagination<Encuestado>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<Encuestado>? paginacion = null;
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
                    paginacion = new Pagination<Encuestado>();
                    paginacion.Records = await scope.QueryAsync<Encuestado>("USP_Paginate_Encuestado", dinamycParams, commandType: CommandType.StoredProcedure);
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

        public async Task<Encuestado> Save(Encuestado encuestado)
        {
            Encuestado? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Provincia>("USP_Registrar_Encuestado",
                    new
                    {

                        @Ci=encuestado.Ci,
                        @Nombre_Completo=encuestado.Nombre_Completo,
                        @Edad=encuestado.Edad,
                        @Sexo=encuestado.Sexo
                    }, commandType: CommandType.StoredProcedure);
                    entity = encuestado;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<Encuestado> Update(Encuestado encuestado)
        {
            Encuestado? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Encuestado>("USP_Actualizar_Encuestado",
                    new
                    {
                        @Ci = encuestado.Ci,
                        @Nombre_Completo = encuestado.Nombre_Completo,
                        @Edad = encuestado.Edad,
                        @Sexo = encuestado.Sexo
                    }, commandType: CommandType.StoredProcedure);
                    entity = encuestado;
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

