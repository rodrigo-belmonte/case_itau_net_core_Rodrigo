using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Entity
{
    public class Fundo
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        private string _cnpj;

        public string Cnpj
        {
            get { return _cnpj; }
            set { _cnpj = value.Trim().Replace(".", "").Replace("-", "").Replace("/", ""); }
        }

        public int CodigoTipo { get; set; }
        public decimal? Patrimonio { get; set; }

       
    }
}
