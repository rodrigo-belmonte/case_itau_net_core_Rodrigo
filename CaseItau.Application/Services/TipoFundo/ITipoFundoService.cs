using CaseItau.Domain.Reponses.Fundo;
using CaseItau.Domain.Reponses.TipoFundo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Application.Services.TipoFundo
{
    public interface ITipoFundoService
    {
        public Task<GetListTipoFundoResponse> GetAllTiposFundo();

    }
}
