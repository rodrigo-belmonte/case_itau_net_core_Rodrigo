using CaseItau.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Reponses.Fundo
{
    public class FundoTipoFundoResponse : BaseResponse
    {
        public FundoTipoFundoResponseModel Fundo { get; set; }
    }
}
