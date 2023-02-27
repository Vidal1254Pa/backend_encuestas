using System;
using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Encuestadora.Backend.API.Controllers.Configuracion
{
    [Route("api/v1/encuesta")]
    [ApiController]
    public class EncuestaController : ControllerBase
    {
        private readonly ILogger<EncuestaController> _logger;
        private readonly EncuestaApp _encuestaApp;
        public EncuestaController(EncuestaApp encuestaApp, ILogger<EncuestaController> logger)
        {
            this._logger = logger;
            this._encuestaApp = encuestaApp;
        }

        [HttpGet]
        [Route("paginate")]
        public async Task<ActionResult> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Encuesta>> status = await _encuestaApp.Paginate(page, size, search, orderBy, orderDir);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save(Encuesta encuesta)
        {
            var status = await _encuestaApp.Save(encuesta);

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromBody] Encuesta encuesta)
        {
            encuesta.Id = Id;
            var status = await _encuestaApp.Update(encuesta);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {
            var status = await _encuestaApp.Delete(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<ActionResult> FindById([FromRoute] int Id)
        {
            var status = await _encuestaApp.FindById(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List()
        {
            var status = await _encuestaApp.List();

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }
    }
}

