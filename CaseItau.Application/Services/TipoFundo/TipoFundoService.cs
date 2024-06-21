using CaseItau.Domain.Reponses.Fundo;
using CaseItau.Domain.Reponses.TipoFundo;
using CaseItau.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Application.Services.TipoFundo
{
    public class TipoFundoService : ITipoFundoService
    {
        private readonly ITipoFundoRepository _tipoFundoRepository;

        public TipoFundoService(ITipoFundoRepository tipoFundoRepository)
        {
            _tipoFundoRepository = tipoFundoRepository;
        }
        public async Task<GetListTipoFundoResponse> GetAllTiposFundo()
        {
            var response = new GetListTipoFundoResponse();
            try
            {
                response.TiposFundo = await _tipoFundoRepository.ListAllAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}
