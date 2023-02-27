using System;
using Encuestadora.Backend.Application.Comun;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
using Microsoft.Extensions.Logging;

namespace Encuestadora.Backend.Application.Configuracion
{
	public class SucursalApp : BaseApp<SucursalApp>
    {
        private readonly ISucursalRepository _sucursalRepository;
        public SucursalApp(ISucursalRepository sucursalRepository, ILogger<BaseApp<SucursalApp>> logger) : base(logger)
        {
            this._sucursalRepository = sucursalRepository;
        }
        public async Task<StatusResponse<Pagination<Sucursal>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Sucursal>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _sucursalRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<Sucursal>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Sucursal>> Save(Sucursal sucursal)
        {
            StatusResponse<Sucursal>? entity = null;
            try
            {
                entity = await ProcesoComplejo(() => this._sucursalRepository.Save(sucursal), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Sucursal>(false, "Sucedió un error al recuperar");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Sucursal>> Update(Sucursal sucursal)
        {
            StatusResponse<Sucursal> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _sucursalRepository.Update(sucursal), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Sucursal>(false, "Sucedió un error al actualizar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusSimpleResponse> Delete(int Id)
        {
            return await this.ProcesoSimple(() => _sucursalRepository.Delete(Id), "");
        }
        public async Task<StatusResponse<Sucursal>> FindById(int Id)
        {
            StatusResponse<Sucursal> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _sucursalRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Sucursal>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<IEnumerable<Sucursal>>> List()
        {
            StatusResponse<IEnumerable<Sucursal>> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _sucursalRepository.List(), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<IEnumerable<Sucursal>>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

