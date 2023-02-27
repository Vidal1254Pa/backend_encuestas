using System;
using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Encuestadora.Backend.API.Controllers.Configuracion
{
    [Route("api/v1/escala")]
    [ApiController]
    public class EscalaController : ControllerBase
    {
        private readonly ILogger<EscalaController> _logger;
        private readonly EscalaApp _escalaApp;
        public EscalaController(EscalaApp escalaApp, ILogger<EscalaController> logger)
        {
            this._logger = logger;
            this._escalaApp = escalaApp;
        }

        [HttpGet]
        [Route("paginate")]
        public async Task<ActionResult> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Escala>> status = await _escalaApp.Paginate(page, size, search, orderBy, orderDir);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save(Escala escala)
        {
            var status = await _escalaApp.Save(escala);

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromBody] Escala escala)
        {
            escala.Id = Id;
            var status = await _escalaApp.Update(escala);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {
            var status = await _escalaApp.Delete(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<ActionResult> FindById([FromRoute] int Id)
        {
            var status = await _escalaApp.FindById(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List()
        {
            var status = await _escalaApp.List();

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }
    }
}

