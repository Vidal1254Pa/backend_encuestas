using System;
using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Encuestadora.Backend.API.Controllers.Configuracion
{
    [Route("api/v1/ciudad")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly ILogger<CiudadController> _logger;
        private readonly CiudadApp _ciudadApp;
        public CiudadController(CiudadApp ciudadApp, ILogger<CiudadController> logger)
        {
            this._logger = logger;
            this._ciudadApp = ciudadApp;
        }

        [HttpGet]
        [Route("paginate")]
        public async Task<ActionResult> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Ciudad>> status = await _ciudadApp.Paginate(page, size, search, orderBy, orderDir);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save(Ciudad ciudad)
        {
            var status = await _ciudadApp.Save(ciudad);

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromBody] Ciudad ciudad)
        {
            ciudad.Id = Id;
            var status = await _ciudadApp.Update(ciudad);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {
            var status = await _ciudadApp.Delete(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<ActionResult> FindById([FromRoute] int Id)
        {
            var status = await _ciudadApp.FindById(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List()
        {
            var status = await _ciudadApp.List();

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }
    }
}

