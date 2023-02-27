using System;
using Encuestadora.Backend.API.Controllers.Configuracion;
using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Application.Requerimiento;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Domain.Requerimiento.Domain;
using Encuestadora.Backend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Encuestadora.Backend.API.Controllers.Requerimiento
{
    [Route("api/v1/create-encuesta")]
    [ApiController]
    public class EncuestaRequerimietoController : ControllerBase
    {
        private readonly ILogger<EncuestaRequerimietoController> _logger;
        private readonly EncuestaRequerimientoApp _encuestaRequerimientoApp;
        public EncuestaRequerimietoController(EncuestaRequerimientoApp encuestaRequerimientoApp, ILogger<EncuestaRequerimietoController> logger)
        {
            this._logger = logger;
            this._encuestaRequerimientoApp = encuestaRequerimientoApp;
        }

        [HttpGet]
        [Route("paginate")]
        public async Task<ActionResult> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<EncuestaRequerimiento>> status = await _encuestaRequerimientoApp.Paginate(page, size, search, orderBy, orderDir);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save(EncuestaRequerimiento encuestaRequerimiento)
        {
            var status = await _encuestaRequerimientoApp.Save(encuestaRequerimiento);

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<ActionResult> FindById([FromRoute] int Id)
        {
            var status = await _encuestaRequerimientoApp.FindById(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
    }
}

