using CaseItau.Domain.Models;
using CaseItau.Domain.Reponses;
using CaseItau.Domain.Reponses.Fundo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntity = CaseItau.Domain.Entity;
namespace CaseItau.Application.Services.Fundo
{
    public interface IFundoService
    {
        public Task<FundoTipoFundoResponse> CreateFundo(DomainEntity.Fundo fundo );
        public Task<FundoTipoFundoResponse> UpdateFundo(string codigo, UpdateFundoRequestModel fundo );
        public Task<BaseResponse> DeleteFundo(string codigo);
        public Task<FundoTipoFundoResponse> GetFundoById(string codigo);
        public Task<GetListFundosTipoFundoResponse> GetAllFundos();
        public Task<FundoTipoFundoResponse> MovimentarFundo(string codigo, decimal movimentacao);

    }
}
