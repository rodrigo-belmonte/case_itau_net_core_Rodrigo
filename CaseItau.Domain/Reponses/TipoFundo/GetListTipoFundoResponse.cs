using CaseItau.Domain.Reponses.Fundo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntity = CaseItau.Domain.Entity;
namespace CaseItau.Domain.Reponses.TipoFundo
{
    public class GetListTipoFundoResponse: BaseResponse
    {
        public IEnumerable<DomainEntity.TipoFundo> TiposFundo { get; set; }

    }
}
