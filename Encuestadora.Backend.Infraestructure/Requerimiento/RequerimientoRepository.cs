using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Requerimiento.Domain;
using Encuestadora.Backend.Domain.Requerimiento.Interfaces;
using Encuestadora.Backend.Shared;

namespace Encuestadora.Backend.Infraestructure.Requerimiento
{
	public class RequerimientoRepository:IEncuestaRequerimientoRepository
	{
        protected readonly ICustomConnection _connection;
        public RequerimientoRepository(ICustomConnection connection)
        {
            this._connection = connection;
        }

        public async Task<EncuestaRequerimiento> FindById(int id)
        {
            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("Id", id);
            EncuestaRequerimiento encuestaRequerimiento = new EncuestaRequerimiento();
            List<DetalleEncuesta> Encuesta = new List<DetalleEncuesta>();
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    await scope.QueryAsync<EncuestaRequerimiento, DetalleEncuesta,Encuesta,Encuestado, EncuestaRequerimiento>("USP_Listar_EncuestaRequerimiento_X_ID",
                    (ER, DE, E, E2) =>
                    {
                        EncuestaRequerimiento encuestaRequerimiento1 = null;
                        if (ER != null )
                        {
                            encuestaRequerimiento1 = ER;
                            if (!Encuesta.Any(x => x.Encuesta_Id == DE.Encuesta_Id)){
                                Encuesta.Add(DE);
                            }
                            encuestaRequerimiento = ER;
                        }
                        return ER;
                    }, param: parametros, commandType: CommandType.StoredProcedure, splitOn: "EncuestaRealizada_Id,Id,Ci");
                    encuestaRequerimiento.DetalleEncuestas = Encuesta;
                }
                catch (Exception ex)
                {
                    throw new CustomException("Sucedió un error al realizar la operación", ex);
                }
            }
            return encuestaRequerimiento;
        }

        public async Task<Pagination<EncuestaRequerimiento>> Paginate(int page, int size, string? search, string? orderBy, string? orderDir)
        {
            Pagination<EncuestaRequerimiento>? paginacion = null;
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
                    paginacion = new Pagination<EncuestaRequerimiento>();
                    paginacion.Records = await scope.QueryAsync<EncuestaRequerimiento>("USP_Paginate_EncuestaRequerimiento", dinamycParams, commandType: CommandType.StoredProcedure);
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

        public async Task<EncuestaRequerimiento> Save(EncuestaRequerimiento encuestaRequerimiento, SqlConnection conexion, SqlTransaction transaccion)
        {
            try
            {
                DynamicParameters parametros = new();

                parametros.Add("Encuestado_Id", encuestaRequerimiento.Encuestado_Id);
                parametros.Add("Sucursal_Id", encuestaRequerimiento.Sucursal_Id);
                var result=await conexion.QueryAsync<int>("USP_Registrar_EncuestaRequerimiento", parametros, transaccion, commandType: CommandType.StoredProcedure);
                encuestaRequerimiento.Id = result.Single();
            }
            catch (Exception ex)
            {
                throw new CustomException("Sucedió un error al registrar la relacion entre Cotización y Configurable", ex);
            }

            return encuestaRequerimiento;
        }

        public async Task<int> Save_Detalle(DetalleEncuesta detalleEncuesta, SqlConnection conexion, SqlTransaction transaccion)
        {
            try
            {
                DynamicParameters parametros = new();

                parametros.Add("Encuesta_Id", detalleEncuesta.Encuesta_Id);
                parametros.Add("EncuestaRealizada_Id", detalleEncuesta.EncuestaRealizada_Id);
                parametros.Add("Respuesta", detalleEncuesta.Respuesta);
                await conexion.QueryAsync<int>("USP_Registrar_DetalleEncuesta", parametros, transaccion, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new CustomException("Sucedió un error al registrar la relacion entre Cotización y Configurable", ex);
            }

            return 1;
        }
    }
}

