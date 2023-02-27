using System;
using Encuestadora.Backend.Application.Comun;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Shared;
using Microsoft.Extensions.Logging;

namespace Encuestadora.Backend.Application.Configuracion
{
	public class EncuestadoApp : BaseApp<EncuestadoApp>
    {
        private readonly IEncuestadoRepository _encuestadoRepository;
        public EncuestadoApp(IEncuestadoRepository encuestadoRepository, ILogger<BaseApp<EncuestadoApp>> logger) : base(logger)
        {
            this._encuestadoRepository = encuestadoRepository;
        }
        public async Task<StatusResponse<Pagination<Encuestado>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Encuestado>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _encuestadoRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<Encuestado>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Encuestado>> Save(Encuestado encuestado)
        {
            StatusResponse<Encuestado>? entity = null;
            try
            {
                entity = await ProcesoComplejo(() => this._encuestadoRepository.Save(encuestado), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Encuestado>(false, "Sucedió un error al recuperar");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Encuestado>> Update(Encuestado encuestado)
        {
            StatusResponse<Encuestado> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _encuestadoRepository.Update(encuestado), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Encuestado>(false, "Sucedió un error al actualizar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusSimpleResponse> Delete(string Id)
        {
            return await this.ProcesoSimple(() => _encuestadoRepository.Delete(Id), "");
        }
        public async Task<StatusResponse<Encuestado>> FindById(string Id)
        {
            StatusResponse<Encuestado> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _encuestadoRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Encuestado>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<IEnumerable<Encuestado>>> List()
        {
            StatusResponse<IEnumerable<Encuestado>> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _encuestadoRepository.List(), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<IEnumerable<Encuestado>>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

