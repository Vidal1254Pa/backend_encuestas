using System;
using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Encuestadora.Backend.API.Controllers.Configuracion
{
    [Route("api/v1/provincia")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ILogger<ProvinciaController> _logger;
        private readonly ProvinciaApp _provinciaApp;
        public ProvinciaController(ProvinciaApp provinciaApp, ILogger<ProvinciaController> logger)
        {
            this._logger = logger;
            this._provinciaApp = provinciaApp;
        }

        [HttpGet]
        [Route("paginate")]
        public async Task<ActionResult> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Provincia>> status = await _provinciaApp.Paginate(page, size, search, orderBy, orderDir);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save(Provincia provincia)
        {
            var status = await _provinciaApp.Save(provincia);

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromBody] Provincia provincia)
        {
            provincia.Id = Id;
            var status = await _provinciaApp.Update(provincia);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {
            var status = await _provinciaApp.Delete(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<ActionResult> FindById([FromRoute] int Id)
        {
            var status = await _provinciaApp.FindById(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List()
        {
            var status = await _provinciaApp.List();

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }
    }
}

