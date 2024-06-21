using AutoMapper;
using CaseItau.Application.Utils;
using CaseItau.Domain.Entity;
using CaseItau.Domain.Models;
using CaseItau.Domain.Reponses;
using CaseItau.Domain.Reponses.Fundo;
using CaseItau.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Application.Services.Fundo
{
    public class FundoService : IFundoService
    {
        private readonly IFundoRepository _fundoRepository;
        private readonly ITipoFundoRepository _tipoFundoRepository;
        private readonly IMapper _mapper;

        public FundoService(IFundoRepository fundoRepository, ITipoFundoRepository tipoFundoRepository, IMapper mapper)
        {
            _fundoRepository = fundoRepository;
            _tipoFundoRepository = tipoFundoRepository;
            _mapper = mapper;
        }
        public async Task<FundoTipoFundoResponse> CreateFundo(Domain.Entity.Fundo fundo)
        {
            var response = new FundoTipoFundoResponse();
            try
            {

                var validations = await CreateValidationsAsync(fundo);

                if (validations.Any())
                {
                    response.Success = false;
                    response.Errors = validations;
                    response.Message = "Não foi possível realizar o Cadastro, Verifique os Erros";
                    return response;
                }

                await _fundoRepository.InsertAsync(fundo);

                response.Fundo = _mapper.Map<FundoTipoFundoResponseModel>(await _fundoRepository.GetByIdAsync(fundo.Codigo));

                response.Message = "Cadastro Realizado com Sucesso";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteFundo(string codigo)
        {
            var response = new BaseResponse();
            try
            {
                var fundoExists = await _fundoRepository.CodigoFundoExistsAsync(codigo);
                if (!fundoExists)
                {
                    response.Success = false;
                    response.Message = "Não foi possível a exclusão, Verifique os Erros";
                    response.Errors.Add("Codigo do Fundo não encontrado.");
                    return response;
                }

                await _fundoRepository.DeleteAsync(codigo);

                response.Message = "Fundo Excluído com Sucesso";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<GetListFundosTipoFundoResponse> GetAllFundos()
        {
            var response = new GetListFundosTipoFundoResponse();
            try
            {
                response.Fundos = _mapper.Map<IEnumerable<FundoTipoFundoResponseModel>>(await _fundoRepository.ListAllAsync());
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<FundoTipoFundoResponse> GetFundoById(string codigo)
        {
            var response = new FundoTipoFundoResponse();
            try
            {
                response.Fundo = _mapper.Map<FundoTipoFundoResponseModel>(await _fundoRepository.GetByIdAsync(codigo));
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;

        }

        public async Task<FundoTipoFundoResponse> MovimentarFundo(string codigo, decimal movimentacao)
        {
            var response = new FundoTipoFundoResponse();
            try
            {
                var validations = await MovimentarFundoValidations(codigo, movimentacao);

                if (validations.Any())
                {
                    response.Success = false;
                    response.Errors = validations;
                    response.Message = "Não foi possível realizar a movimentação, Verifique os Erros";
                    return response;
                }

                await _fundoRepository.MovimentarPatrimonioAsync(codigo, movimentacao);
                response.Fundo = _mapper.Map<FundoTipoFundoResponseModel>(await _fundoRepository.GetByIdAsync(codigo));

                response.Message = "Movimentação realizada com Sucesso";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        public async Task<FundoTipoFundoResponse> UpdateFundo(string codigo, UpdateFundoRequestModel request)
        {
            var response = new FundoTipoFundoResponse();
            try
            {
                var fundo = _mapper.Map<UpdateFundoModel>(request); 
                fundo.Codigo = codigo;

                var fundoToUpdate = _mapper.Map<FundoTipoFundoResponseModel>(await _fundoRepository.GetByIdAsync(codigo));

                fundo.Cnpj = string.IsNullOrEmpty(fundo.Cnpj) ? fundoToUpdate.Cnpj : fundo.Cnpj; 
                fundo.Nome = string.IsNullOrEmpty(fundo.Nome) ? fundoToUpdate.Nome : fundo.Nome; 
                fundo.CodigoTipo = fundo.CodigoTipo == 0 ? fundoToUpdate.CodigoTipo : fundo.CodigoTipo;

                var validations = await UpdateValidationsAsync(fundo);

                if (validations.Any())
                {
                    response.Success = false;
                    response.Errors = validations;
                    response.Message = "Não foi possível realizar a alteração, Verifique os Erros";
                    return response;
                }
                await _fundoRepository.UpdateAsync(fundo);

                response.Fundo = _mapper.Map<FundoTipoFundoResponseModel>(await _fundoRepository.GetByIdAsync(codigo));

                response.Message = "Fundo Atualizado com Sucesso";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }

        private async Task<List<string>> CreateValidationsAsync(Domain.Entity.Fundo fundo)
        {
            List<string> errors = new List<string>();


            if (String.IsNullOrEmpty(fundo.Codigo))
                errors.Add("Codigo é obrigatório;");
            else
            {
                if (await _fundoRepository.CodigoFundoExistsAsync(fundo.Codigo))
                    errors.Add("Codigo do Fundo Já Cadastrado;");
            }

            if (fundo.CodigoTipo == 0)
                errors.Add("CodigoTipo é obrigatório;");
            else
            {
                var tipoFundoExists = await _tipoFundoRepository.TipoFundoExistsAsync(fundo.CodigoTipo);
                if (!tipoFundoExists)
                    errors.Add("CodigoTipo não encontrado;");
            }

            if (String.IsNullOrEmpty(fundo.Nome))
                errors.Add("Nome é obrigatório;");


            if (String.IsNullOrEmpty(fundo.Cnpj))
                errors.Add("CNPJ é obrigatório;");
            else
            {
                if (!ValidaCNPJ.IsCnpj(fundo.Cnpj))
                    errors.Add("Favor fornecer um CNPJ válido;");

                if (await _fundoRepository.CnpjFundoExistsAsync(fundo.Cnpj))
                    errors.Add("CNPJ Já Cadastrado em outro Fundo;");
            }

            return errors;
        }

        private async Task<List<string>> UpdateValidationsAsync( UpdateFundoModel fundo)
        {
            List<string> errors = new List<string>();

            var fundoExists = await _fundoRepository.CodigoFundoExistsAsync(fundo.Codigo);
            if (!fundoExists)
                errors.Add("Codigo do Fundo não encontrado;");

            var tipoFundoExists = await _tipoFundoRepository.TipoFundoExistsAsync(fundo.CodigoTipo);
            if (!tipoFundoExists)
                errors.Add("CodigoTipo não encontrado;");


            if (!ValidaCNPJ.IsCnpj(fundo.Cnpj))
                errors.Add("Favor fornecer um CNPJ válido;");

            var cnpjExists = await _fundoRepository.CnpjFundoExistsAsync(fundo.Cnpj, fundo.Codigo);

            if (cnpjExists)
                errors.Add("CNPJ Já Cadastrado em outro Fundo;");


            return errors;
        }

        private async Task<List<string>> MovimentarFundoValidations(string codigo, decimal movimentacao)
        {
            List<string> errors = new List<string>();

            var fundoExists = await _fundoRepository.CodigoFundoExistsAsync(codigo);
            if (!fundoExists)
            {
                errors.Add("Codigo do Fundo não encontrado.");
            }

            if (movimentacao <= 0)
                errors.Add("Valor da movimentação deve ser maior que 0");

            return errors;
        }

    }
}
