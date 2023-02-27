using System;
using Encuestadora.Backend.Application.Comun;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
using Microsoft.Extensions.Logging;

namespace Encuestadora.Backend.Application.Configuracion
{
	public class ProvinciaApp : BaseApp<ProvinciaApp>
    {
        private readonly IProvinciaRepository _provinciaRepository;
        public ProvinciaApp(IProvinciaRepository provinciaRepository, ILogger<BaseApp<ProvinciaApp>> logger) : base(logger)
        {
            this._provinciaRepository = provinciaRepository;
        }
        public async Task<StatusResponse<Pagination<Provincia>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Provincia>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _provinciaRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<Provincia>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Provincia>> Save(Provincia provincia)
        {
            StatusResponse<Provincia>? entity = null;
            try
            {
                entity = await ProcesoComplejo(() => this._provinciaRepository.Save(provincia), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Provincia>(false, "Sucedió un error al recuperar");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Provincia>> Update(Provincia provincia)
        {
            StatusResponse<Provincia> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _provinciaRepository.Update(provincia), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Provincia>(false, "Sucedió un error al actualizar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusSimpleResponse> Delete(int Id)
        {
            return await this.ProcesoSimple(() => _provinciaRepository.Delete(Id), "");
        }
        public async Task<StatusResponse<Provincia>> FindById(int Id)
        {
            StatusResponse<Provincia> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _provinciaRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Provincia>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<IEnumerable<Provincia>>> List()
        {
            StatusResponse<IEnumerable<Provincia>> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _provinciaRepository.List(), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<IEnumerable<Provincia>>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

