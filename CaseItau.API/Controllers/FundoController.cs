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
using System.Net;

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

        /// <summary>
        /// Retorna lista de todos os Fundos Cadastrados
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso - Retorna todos os Fundos</response>
        /// <response code="400">Erro - Retorna Lista de Erros de Validação</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetListFundosTipoFundoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetListFundosTipoFundoResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetListFundosTipoFundoResponse>> Get()
        {
            var response = await _fundoService.GetAllFundos();
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Retorna Fundo A partir do Codigo
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso - Retorna um fundo </response>
        /// <response code="400">Erro - Retorna Lista de Erros de Validação</response>
        [HttpGet("{codigo}", Name = "Get")]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<FundoTipoFundoResponse>> Get(string codigo)
        {
            var response = await _fundoService.GetFundoById(codigo);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Cadastro de Fundo 
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Sucesso - Retorna Fundo Cadastrado</response>
        /// <response code="400">Erro - Retorna Lista de Erros de Validação</response>        
        [HttpPost]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.BadRequest)]
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

        /// <summary>
        /// Alteração de Fundo
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso - Retorna Fundo Atualizado</response>
        /// <response code="400">Erro - Retorna Lista de Erros de Validação</response>        
        [HttpPut("{codigo}")]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<FundoTipoFundoResponse>> Put(string codigo, [FromBody] UpdateFundoRequestModel fundo)
        {

            var response = await _fundoService.UpdateFundo(codigo, fundo);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        /// <summary>
        /// Exclusão de Fundo
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso - Exclui Fundo </response>
        /// <response code="400">Erro - Retorna Lista de Erros de Validação</response>        
        [HttpDelete("{codigo}")]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BaseResponse>> Delete(string codigo)
        {
            var response = await _fundoService.DeleteFundo(codigo);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Movimentação de Patrimonio
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso - Retorna fundo com Patrimonio Atualizado</response>
        /// <response code="400">Erro - Retorna Lista de Erros de Validação</response>
        [HttpPut("{codigo}/patrimonio")]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FundoTipoFundoResponse), (int)HttpStatusCode.BadRequest)]
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
