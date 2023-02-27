using System;
using Encuestadora.Backend.Application.Comun;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
using Microsoft.Extensions.Logging;

namespace Encuestadora.Backend.Application.Configuracion
{
	public class CiudadApp : BaseApp<CiudadApp>
    {
        private readonly ICiudadRepository _ciudadRepository;
        public CiudadApp(ICiudadRepository ciudadRepository, ILogger<BaseApp<CiudadApp>> logger) : base(logger)
        {
            this._ciudadRepository = ciudadRepository;
        }
        public async Task<StatusResponse<Pagination<Ciudad>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Ciudad>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _ciudadRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<Ciudad>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Ciudad>> Save(Ciudad ciudad)
        {
            StatusResponse<Ciudad>? entity = null;
            try
            {
                entity = await ProcesoComplejo(() => this._ciudadRepository.Save(ciudad), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Ciudad>(false, "Sucedió un error al recuperar");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Ciudad>> Update(Ciudad ciudad)
        {
            StatusResponse<Ciudad> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _ciudadRepository.Update(ciudad), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Ciudad>(false, "Sucedió un error al actualizar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusSimpleResponse> Delete(int Id)
        {
            return await this.ProcesoSimple(() => _ciudadRepository.Delete(Id), "");
        }
        public async Task<StatusResponse<Ciudad>> FindById(int Id)
        {
            StatusResponse<Ciudad> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _ciudadRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Ciudad>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<IEnumerable<Ciudad>>> List()
        {
            StatusResponse<IEnumerable<Ciudad>> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _ciudadRepository.List(), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<IEnumerable<Ciudad>>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

