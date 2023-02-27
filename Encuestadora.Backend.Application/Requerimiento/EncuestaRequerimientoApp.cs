using System;
using Encuestadora.Backend.Application.Comun;
using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Requerimiento.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Domain.Requerimiento.Interfaces;
using Encuestadora.Backend.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Encuestadora.Backend.Application.Requerimiento
{
	public class EncuestaRequerimientoApp : BaseApp<EncuestaRequerimientoApp>
    {

        private readonly IEncuestaRequerimientoRepository _encuestaRequerimientoRepository;
        public EncuestaRequerimientoApp(IEncuestaRequerimientoRepository encuestaRequerimientoRepository, ILogger<BaseApp<EncuestaRequerimientoApp>> logger, IConfiguration config) : base(logger, config)
        {
            this._encuestaRequerimientoRepository = encuestaRequerimientoRepository;
        }
        public async Task<StatusResponse<Pagination<EncuestaRequerimiento>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<EncuestaRequerimiento>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _encuestaRequerimientoRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<EncuestaRequerimiento>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<EncuestaRequerimiento>> Save(EncuestaRequerimiento encuestaRequerimiento)
        {
            StatusResponse<EncuestaRequerimiento>? entity = null;
            try
            {
                using (SqlConnection conexion = ConexionParaTransaccion())
                {
                    conexion.Open();

                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {

                        entity = await ProcesoComplejo(() => _encuestaRequerimientoRepository.Save(encuestaRequerimiento, conexion, transaccion), "");
                        foreach (DetalleEncuesta item in encuestaRequerimiento.DetalleEncuestas)
                        {
                            int rowsAffected = -1;
                            item.EncuestaRealizada_Id= entity.Data.Id;

                            rowsAffected = await _encuestaRequerimientoRepository.Save_Detalle(item, conexion, transaccion);
                            if (rowsAffected < 0)
                            {
                                entity.Titulo = "No se pudo registrar la relacion";
                                return entity;
                            }
                        }
                        transaccion.Commit();
                        entity.Satisfactorio = true;
                        entity.Data = encuestaRequerimiento;

                    }
                }
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<EncuestaRequerimiento>(false, "Sucedió un error al registrar informacion de la temperatura");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }

        public async Task<StatusResponse<EncuestaRequerimiento>> FindById(int Id)
        {
            StatusResponse<EncuestaRequerimiento> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _encuestaRequerimientoRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<EncuestaRequerimiento>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

