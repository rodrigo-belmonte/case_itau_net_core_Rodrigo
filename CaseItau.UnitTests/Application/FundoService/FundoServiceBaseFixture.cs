using Bogus;
using Bogus.Extensions.Brazil;
using CaseItau.Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.UnitTests.Application.FundoService
{
    public class FundoServiceBaseFixture
    {
        public Faker Faker { get; set; }
        public FundoServiceBaseFixture()
        {
            Faker = new Faker("pt_BR");
        }
        public Domain.Entity.Fundo GetInput()
        {
            return GetValidFundo();
        }

        public Domain.Entity.Fundo GetValidFundo()
        {
            return new Domain.Entity.Fundo
            {
                Codigo = $"ITAU{Faker.Random.String2(2)}{Faker.Random.Int(100, 999)}",
                Cnpj = Faker.Company.Cnpj(),
                Nome = Faker.Company.CompanyName(),
                CodigoTipo = 1,
                Patrimonio = Faker.Random.Decimal()
            };
        }

        public Domain.Entity.Fundo GetCnpjNull()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.Cnpj = null;

            return invalidFundo;
        }
        public Domain.Entity.Fundo GetInvalidCnpj()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.Cnpj = Faker.Random.String2(10, 16, "01234567890");

            return invalidFundo;
        }

        public Domain.Entity.Fundo GetCnpjAlreadyExist()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.Cnpj = "12222333444455";

            return invalidFundo;
        }

        public Domain.Entity.Fundo GetCodigoNull()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.Codigo = null;

            return invalidFundo;
        }

        public Domain.Entity.Fundo GetCodigoAlreadyExist()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.Codigo = "ITAURF123";

            return invalidFundo;
        }

        public Domain.Entity.Fundo GetCodigoTipoZero()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.CodigoTipo = 0;

            return invalidFundo;
        }
        public Domain.Entity.Fundo GetCodigoTipoNotExist()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.CodigoTipo = Faker.Random.Int(4, 10);

            return invalidFundo;
        }

        public Domain.Entity.Fundo GetNomeNull()
        {
            var invalidFundo = GetValidFundo();
            invalidFundo.Nome = null;

            return invalidFundo;
        }

    }
}
