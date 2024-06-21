using CaseItau.Domain.Entity;
using CaseItau.Domain.Models;
using CaseItau.Domain.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data.Repositories
{
    public class FundoRepository : IFundoRepository
    {
        private readonly CaseItauContext _context;

        public FundoRepository(CaseItauContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(string codigo)
        {
            var query = "DELETE FROM FUNDO WHERE CODIGO = @codigo";
            var param = new { codigo };
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task<bool> CodigoFundoExistsAsync(string codigo)
        {
            var query = "SELECT CODIGO FROM FUNDO WHERE CODIGO = @codigo";
            var param = new { codigo };
            using (var connection = _context.CreateConnection())
            {
                return string.IsNullOrEmpty(await connection.QuerySingleOrDefaultAsync<string>(query, param))  ? false : true;
            }
        }
        public async Task<GetFundoTipoFundoModel> GetByIdAsync(string codigo)
        {
            var query = "SELECT F.CODIGO, F.NOME, F.CNPJ, F.CODIGO_TIPO AS CODIGOTIPO, F.PATRIMONIO, T.NOME AS NomeTipo FROM FUNDO F INNER JOIN TIPO_FUNDO T ON T.CODIGO = F.CODIGO_TIPO WHERE F.CODIGO = @codigo";
            var param = new { codigo };
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<GetFundoTipoFundoModel>(query, param);
            }
        }

        public async Task InsertAsync(Fundo fundo)
        {
            var query = "INSERT INTO FUNDO (CODIGO, NOME, CNPJ, CODIGO_TIPO, PATRIMONIO)  VALUES (@Codigo, @Nome, @Cnpj, @CodigoTipo, @Patrimonio)";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, fundo);
            }
        }

        public async Task<IEnumerable<GetFundoTipoFundoModel>> ListAllAsync()
        {
            var query = "SELECT F.CODIGO, F.NOME, F.CNPJ, F.CODIGO_TIPO AS CODIGOTIPO, F.PATRIMONIO, T.NOME AS NomeTipo FROM FUNDO F INNER JOIN TIPO_FUNDO T ON T.CODIGO = F.CODIGO_TIPO";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<GetFundoTipoFundoModel>(query);
            }
        }

        public async Task MovimentarPatrimonioAsync(string codigo, decimal movimentacao)
        {
            var query = "UPDATE FUNDO SET PATRIMONIO = IFNULL(PATRIMONIO,0) + @movimentacao WHERE CODIGO = @codigo ";
            var param = new { codigo, movimentacao };
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, param);
            }
        }

        public async Task UpdateAsync(UpdateFundoModel fundo)
        {
            var query = "UPDATE FUNDO SET Nome = @Nome , CNPJ = @Cnpj , CODIGO_TIPO = @CodigoTipo  WHERE CODIGO =  @Codigo ";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, fundo);
            }
        }

        public async Task<bool> CnpjFundoExistsAsync(string cnpj)
        {
            var query = "SELECT CODIGO FROM FUNDO WHERE CNPJ = @cnpj";
            var param = new { cnpj };
            using (var connection = _context.CreateConnection())
            {
                return string.IsNullOrEmpty(await connection.QuerySingleOrDefaultAsync<string>(query, param)) ? false : true;
            }
        }

        public async Task<bool> CnpjFundoExistsAsync(string cnpj, string codigo)
        {
            var query = "SELECT CODIGO FROM FUNDO WHERE CNPJ = @cnpj and CODIGO != @codigo";
            var param = new { cnpj, codigo };
            using (var connection = _context.CreateConnection())
            {
                return string.IsNullOrEmpty(await connection.QuerySingleOrDefaultAsync<string>(query, param)) ? false : true;
            }
        }

    }
}
