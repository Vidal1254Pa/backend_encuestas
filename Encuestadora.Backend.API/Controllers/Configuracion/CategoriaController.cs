using System;
using Microsoft.AspNetCore.Mvc;
using Encuestadora.Backend.Shared;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Application.Configuracion;
namespace Encuestadora.Backend.API.Controllers.Configuracion
{
    [Route("api/v1/categoria")]
    [ApiController]
    public class CategoriaController: ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly CategoriaApp _categoriaApp;
        public CategoriaController(CategoriaApp categoriaApp, ILogger<CategoriaController> logger)
        {
            this._logger = logger;
            this._categoriaApp = categoriaApp;
        }

        [HttpGet]
        [Route("paginate")]
        public async Task<ActionResult> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Categoria>> status = await _categoriaApp.Paginate(page, size, search, orderBy, orderDir);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save(Categoria categoria)
        {
            var status = await _categoriaApp.Save(categoria);

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromBody] Categoria categoria)
        {
            categoria.Id = Id;
            var status = await _categoriaApp.Update(categoria);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {
            var status = await _categoriaApp.Delete(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<ActionResult> FindById([FromRoute] int Id)
        {
            var status = await _categoriaApp.FindById(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List()
        {
            var status = await _categoriaApp.List();

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }
    }
}

