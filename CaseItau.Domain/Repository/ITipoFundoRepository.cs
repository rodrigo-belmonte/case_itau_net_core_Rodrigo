using CaseItau.Domain.Entity;
using CaseItau.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Domain.Repository
{
    public interface ITipoFundoRepository
    {
        public Task<IEnumerable<TipoFundo>> ListAllAsync();

        public Task<bool> TipoFundoExistsAsync(int codigo);

    }
}
