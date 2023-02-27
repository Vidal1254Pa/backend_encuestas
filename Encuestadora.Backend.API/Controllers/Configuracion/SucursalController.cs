using System;
using Encuestadora.Backend.Application.Configuracion;
using Encuestadora.Backend.Domain.Configuracion.Domain;
using Encuestadora.Backend.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Encuestadora.Backend.API.Controllers.Configuracion
{
    [Route("api/v1/sucursal")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ILogger<SucursalController> _logger;
        private readonly SucursalApp _sucursalApp;
        public SucursalController(SucursalApp sucursalApp, ILogger<SucursalController> logger)
        {
            this._logger = logger;
            this._sucursalApp = sucursalApp;
        }

        [HttpGet]
        [Route("paginate")]
        public async Task<ActionResult> Paginate(int? page, int? size, string? search, string? orderBy, string? orderDir)
        {
            StatusResponse<Pagination<Sucursal>> status = await _sucursalApp.Paginate(page, size, search, orderBy, orderDir);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Save(Sucursal sucursal)
        {
            var status = await _sucursalApp.Save(sucursal);

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult> Update([FromRoute] int Id, [FromBody] Sucursal sucursal)
        {
            sucursal.Id = Id;
            var status = await _sucursalApp.Update(sucursal);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {
            var status = await _sucursalApp.Delete(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }

        [HttpGet]
        [Route("getById/{Id}")]
        public async Task<ActionResult> FindById([FromRoute] int Id)
        {
            var status = await _sucursalApp.FindById(Id);
            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status.Data);
        }
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> List()
        {
            var status = await _sucursalApp.List();

            if (!status.Satisfactorio)
                return StatusCode(StatusCodes.Status500InternalServerError, status);

            return Ok(status);
        }
    }
}

