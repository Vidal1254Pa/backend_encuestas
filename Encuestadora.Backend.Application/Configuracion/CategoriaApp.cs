using System;
using Encuestadora.Backend.Application.Comun;
using Microsoft.Extensions.Logging;
using Encuestadora.Backend.Domain.Configuracion.Interfaces;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
namespace Encuestadora.Backend.Application.Configuracion
{
	public class CategoriaApp : BaseApp<CategoriaApp>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaApp(ICategoriaRepository categoriaRepository, ILogger<BaseApp<CategoriaApp>> logger) : base(logger)
        {
            this._categoriaRepository = categoriaRepository;
        }
        public async Task<StatusResponse<Pagination<Categoria>>> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Categoria>>? entity = null;
            try
            {
                page ??= 1;
                size ??= 10;
                entity = await ProcesoComplejo(() => _categoriaRepository.Paginate(page.Value, size.Value, search, orderBy, orderDir), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Pagination<Categoria>>(false, "Sucedió un error al recuperar informacion del informe");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Categoria>> Save(Categoria categoria)
        {
            StatusResponse<Categoria>? entity = null;
            try
            {
                entity = await ProcesoComplejo(() => this._categoriaRepository.Save(categoria), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Categoria>(false, "Sucedió un error al recuperar");
                _logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<Categoria>> Update(Categoria categoria)
        {
            StatusResponse<Categoria> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _categoriaRepository.Update(categoria), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Categoria>(false, "Sucedió un error al actualizar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusSimpleResponse> Delete(int Id)
        {
            return await this.ProcesoSimple(() => _categoriaRepository.Delete(Id), "");
        }
        public async Task<StatusResponse<Categoria>> FindById(int Id)
        {
            StatusResponse<Categoria> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _categoriaRepository.FindById(Id), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<Categoria>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
        public async Task<StatusResponse<IEnumerable<Categoria>>> List()
        {
            StatusResponse<IEnumerable<Categoria>> entity = null;
            try
            {
                entity = await this.ProcesoComplejo(() => _categoriaRepository.List(), "");
            }
            catch (Exception ex)
            {
                entity = new StatusResponse<IEnumerable<Categoria>>(false, "Sucedió un error al recuperar informacion");
                this._logger.LogError(ex, "Id: {0}", entity.Id);
                entity.Detalle = ex.ToString();
                entity.Satisfactorio = false;
            }
            return entity;
        }
    }
}

