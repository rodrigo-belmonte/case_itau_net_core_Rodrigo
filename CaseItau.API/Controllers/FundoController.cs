using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using CaseItau.Application.Services.Fundo;
using CaseItau.Domain.Models;
using CaseItau.Domain.Entity;
using CaseItau.Domain.Reponses;
using CaseItau.Domain.Reponses.Fundo;

namespace CaseItau.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundoController : ControllerBase
    {
        private readonly IFundoService _fundoService;

        public FundoController(IFundoService fundoService)
        {
            _fundoService = fundoService;
        }
        // GET: api/Fundo
        [HttpGet]
        public async Task<ActionResult<GetListFundosTipoFundoResponse>> Get()
        {
            var response = await _fundoService.GetAllFundos();
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // GET: api/Fundo/ITAUTESTE01
        [HttpGet("{codigo}", Name = "Get")]
        public async Task<ActionResult<FundoTipoFundoResponse>> Get(string codigo)
        {
            var response = await _fundoService.GetFundoById(codigo);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // POST: api/Fundo
        [HttpPost]
        public async Task<ActionResult<FundoTipoFundoResponse>> Post([FromBody] Fundo fundo)
        {
            //Validar Se TipoExiste
            var response = await _fundoService.CreateFundo(fundo);


            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // PUT: api/Fundo/ITAUTESTE01
        [HttpPut("{codigo}")]
        public async Task<ActionResult<FundoTipoFundoResponse>> Put(string codigo, [FromBody] UpdateFundoRequestModel fundo)
        {
            
            var response = await _fundoService.UpdateFundo(codigo, fundo);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
            
        }

        // DELETE: api/Fundo/ITAUTESTE01
        [HttpDelete("{codigo}")]
        public async Task<ActionResult<BaseResponse>> Delete(string codigo)
        {
            var response = await _fundoService.DeleteFundo(codigo);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{codigo}/patrimonio")]
        public async Task<ActionResult<FundoTipoFundoResponse>> MovimentarPatrimonio(string codigo, [FromBody] decimal movimentacao)
        {
            var response = await _fundoService.MovimentarFundo(codigo, movimentacao);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
