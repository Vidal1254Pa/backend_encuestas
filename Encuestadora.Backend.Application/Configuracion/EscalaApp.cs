using System;
using Encuestadora.Backend.Application.Comun;
using Microsoft.Extensions.Logging;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
namespace Encuestadora.Backend.Application.Configuracion
{
	public class EscalaApp : BaseApp<EscalaApp>
    {
        private readonly IEscalaRepository _escalaRepository;
        public EscalaApp(IEscalaRepository escalaRepository, ILogger<BaseApp<EscalaApp>> logger) : base(logger)
        {
            this._escalaRepository = escalaRepository;
        }
        public async Task<StatusResponse<Pagination<Escala>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Escala>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _escalaRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<Escala>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Escala>> Save(Escala escala)
        {
            StatusResponse<Escala>? entity = null;
            try
            {
                entity = await ProcesoComplejo(() => this._escalaRepository.Save(escala), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Escala>(false, "Sucedió un error al recuperar");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Escala>> Update(Escala escala)
        {
            StatusResponse<Escala> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _escalaRepository.Update(escala), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Escala>(false, "Sucedió un error al actualizar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusSimpleResponse> Delete(int Id)
        {
            return await this.ProcesoSimple(() => _escalaRepository.Delete(Id), "");
        }
        public async Task<StatusResponse<Escala>> FindById(int Id)
        {
            StatusResponse<Escala> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _escalaRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Escala>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<IEnumerable<Escala>>> List()
        {
            StatusResponse<IEnumerable<Escala>> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _escalaRepository.List(), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<IEnumerable<Escala>>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

