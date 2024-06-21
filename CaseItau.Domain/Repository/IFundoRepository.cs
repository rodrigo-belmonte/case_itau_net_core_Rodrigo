using CaseItau.Domain.Entity;
using CaseItau.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Repository
{
    public interface IFundoRepository
    {
        public Task<bool> CodigoFundoExistsAsync(string codigo);
        public Task<bool> CnpjFundoExistsAsync(string cnpj);
        public Task<bool> CnpjFundoExistsAsync(string cnpj, string codigo);
        public Task<IEnumerable<GetFundoTipoFundoModel>> ListAllAsync();
        public Task<GetFundoTipoFundoModel> GetByIdAsync(string codigo);

        public Task InsertAsync(Fundo fundo);

        public Task UpdateAsync(UpdateFundoModel fundo);

        public Task DeleteAsync(string codigo);

        public Task MovimentarPatrimonioAsync(string codigo, decimal movimentacao);


    }
}
