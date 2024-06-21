using CaseItau.Application.Services.TipoFundo;
using CaseItau.Domain.Reponses.Fundo;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CaseItau.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoFundoController : ControllerBase
    {
        private readonly ITipoFundoService _tipoFundoService;

        public TipoFundoController(ITipoFundoService tipoFundoService)
        {
            _tipoFundoService = tipoFundoService;
        }

        /// <summary>
        /// Retorna lista de todos os Tipos de Fundo Cadastrados
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso - Retorna todos os Tipos de Fundo</response>
        /// <response code="400">Erro - Retorna Lista de Erros de Validação</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetListFundosTipoFundoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(GetListFundosTipoFundoResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetListFundosTipoFundoResponse>> Get()
        {
            var response = await _tipoFundoService.GetAllTiposFundo();
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
