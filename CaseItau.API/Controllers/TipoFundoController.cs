using CaseItau.Application.Services.TipoFundo;
using CaseItau.Domain.Reponses.Fundo;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
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
