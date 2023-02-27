using System;
using System.Data;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Infraestructure.Configuracion
{
	public class CategoriaRepository : ICategoriaRepository
	{
        protected readonly ICustomConnection _connection;
        public CategoriaRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task Delete(int id)
        {
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    var items = await scope.ExecuteAsync("USP_Eliminar_Categoria",
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

        public async Task<Categoria> FindById(int id)
        {
            Categoria entity = null;
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    entity = await scope.QueryFirstOrDefaultAsync<Categoria>("USP_Buscar_Categoria_x_Id",
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

        public async Task<IEnumerable<Categoria>> List()
        {
            IEnumerable<Categoria> lista;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    lista = await scope.QueryAsync<Categoria>("USP_Listar_Categoria", commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return lista;
        }

        public async Task<Pagination<Categoria>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<Categoria>? paginacion = null;
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
                    paginacion = new Pagination<Categoria>();
                    paginacion.Records = await scope.QueryAsync<Categoria>("USP_Paginate_Categoria", dinamycParams, commandType: CommandType.StoredProcedure);
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

        public async Task<Categoria> Save(Categoria categoria)
        {
            Categoria? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Categoria>("USP_Registrar_Categoria",
                    new
                    {

                        @Detalle=categoria.Detalle
                    }, commandType: CommandType.StoredProcedure);
                    entity = categoria;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<Categoria> Update(Categoria categoria)
        {
            Categoria? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Categoria>("USP_Actualizar_Categoria",
                    new
                    {
                        @Id=categoria.Id,
                        @Detalle=categoria.Detalle
                    }, commandType: CommandType.StoredProcedure);
                    entity = categoria;
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

