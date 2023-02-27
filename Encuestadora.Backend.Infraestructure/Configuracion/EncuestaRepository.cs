using System;
using System.Data;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
using static Dapper.SqlMapper;
using static System.Formats.Asn1.AsnWriter;

namespace Encuestadora.Backend.Infraestructure.Configuracion
{
	public class EncuestaRepository : IEncuestaRepository
	{
        protected readonly ICustomConnection _connection;
        public EncuestaRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task Delete(int id)
        {
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    var items = await scope.ExecuteAsync("USP_Eliminar_Encuesta",
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

        public async Task<Encuesta> FindById(int id)
        {
            Encuesta entity = null;
            using (var scope = await this._connection.BeginConnection())
            {
                try
                {
                    entity = await scope.QueryFirstOrDefaultAsync<Encuesta>("USP_Buscar_Encuesta_x_Id",
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

        public async Task<IEnumerable<Encuesta>> List()
        {
            DynamicParameters parametros = new DynamicParameters();
            
            List<Encuesta> lista=new List<Encuesta>();
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    await scope.QueryAsync<Encuesta, Escala, Encuesta>("USP_Listar_Encuesta",
                    (enc, esc) =>
                {
                Encuesta cotizacion = null;
                if (enc != null && cotizacion==null)
                {
                        cotizacion = enc;
                        cotizacion.Escala = esc;
                        lista.Add(enc);
                }
                return enc;
            }, param: parametros, commandType: CommandType.StoredProcedure, splitOn: "Id");
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return lista;
        }

        public async Task<Pagination<Encuesta>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<Encuesta>? paginacion = null;
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
                    paginacion = new Pagination<Encuesta>();
                    paginacion.Records = await scope.QueryAsync<Encuesta>("USP_Paginate_Encuesta", dinamycParams, commandType: CommandType.StoredProcedure);
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

        public async Task<Encuesta> Save(Encuesta encuesta)
        {
            Encuesta? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Encuesta>("USP_Registrar_Encuesta",
                    new
                    {

                        @Pregunta=encuesta.Pregunta,
                        @Categoria_Id=encuesta.Categoria_Id,
                        @Escala_Id=encuesta.Escala_Id
                    }, commandType: CommandType.StoredProcedure);
                    entity = encuesta;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return entity;
        }

        public async Task<Encuesta> Update(Encuesta encuesta)
        {
            Encuesta? entity = null;
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    var items = await scope.QueryAsync<Encuesta>("USP_Actualizar_Encuesta",
                    new
                    {
                        @Id = encuesta.Id,
                        @Pregunta = encuesta.Pregunta,
                        @Categoria_Id = encuesta.Categoria_Id,
                        @Escala_Id = encuesta.Escala_Id
                    }, commandType: CommandType.StoredProcedure);
                    entity = encuesta;
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

