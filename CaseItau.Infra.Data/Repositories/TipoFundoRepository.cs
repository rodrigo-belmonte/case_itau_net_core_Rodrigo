using CaseItau.Domain.Entity;
using CaseItau.Domain.Models;
using CaseItau.Domain.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Repositories
{
    public class TipoFundoRepository : ITipoFundoRepository
    {
        private readonly CaseItauContext _context;

        public TipoFundoRepository(CaseItauContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoFundo>> ListAllAsync()
        {
            var query = "SELECT T.CODIGO as CodigoTipo, T.NOME AS NomeTipo FROM  TIPO_FUNDO T";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<TipoFundo>(query);
            }
        }

        public async Task<bool> TipoFundoExistsAsync(int codigo)
        {
            var query = "SELECT CODIGO FROM TIPO_FUNDO WHERE CODIGO = @codigo";
            var param = new { codigo };
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<int>(query, param) == 0 ? false : true;
            }
        }
    }
}
