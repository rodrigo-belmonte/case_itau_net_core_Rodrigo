using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Reponses.Fundo
{
    public class FundoTipoFundoResponseModel
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
        public string NomeTipo { get; set; }
        public decimal? Patrimonio { get; set; }
    }
}
