using System;
using Encuestadora.Backend.Application.Comun;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
using Microsoft.Extensions.Logging;

namespace Encuestadora.Backend.Application.Configuracion
{
	public class EncuestaApp : BaseApp<EncuestaApp>
    {
        private readonly IEncuestaRepository _encuestaRepository;
        public EncuestaApp(IEncuestaRepository encuestaRepository, ILogger<BaseApp<EncuestaApp>> logger) : base(logger)
        {
            this._encuestaRepository = encuestaRepository;
        }
        public async Task<StatusResponse<Pagination<Encuesta>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Encuesta>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _encuestaRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<Encuesta>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Encuesta>> Save(Encuesta encuesta)
        {
            StatusResponse<Encuesta>? entity = null;
            try
            {
                entity = await ProcesoComplejo(() => this._encuestaRepository.Save(encuesta), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Encuesta>(false, "Sucedió un error al recuperar");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Encuesta>> Update(Encuesta encuesta)
        {
            StatusResponse<Encuesta> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _encuestaRepository.Update(encuesta), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Encuesta>(false, "Sucedió un error al actualizar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusSimpleResponse> Delete(int Id)
        {
            return await this.ProcesoSimple(() => _encuestaRepository.Delete(Id), "");
        }
        public async Task<StatusResponse<Encuesta>> FindById(int Id)
        {
            StatusResponse<Encuesta> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _encuestaRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Encuesta>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<IEnumerable<Encuesta>>> List()
        {
            StatusResponse<IEnumerable<Encuesta>> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _encuestaRepository.List(), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<IEnumerable<Encuesta>>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

