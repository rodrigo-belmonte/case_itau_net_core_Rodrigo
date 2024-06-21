using AutoMapper;
using CaseItau.Application.Map;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Services = CaseItau.Application.Services.Fundo;
namespace CaseItau.UnitTests.Application.FundoService
{
    public class FundoServiceTest
    {
        private readonly FundoServiceBaseFixture _fixture;

        public FundoServiceTest(FundoServiceBaseFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact(DisplayName = nameof(CreateFundo))]
        [Trait("Application", "CreateFundo - Services")]
        public async void CreateFundo()
        {
            var fundoRepositoryMock = _fixture.GetFundoRepositoryMock();
            var tipoFundoRepositoryMock = _fixture.GetTipoFundoRepositoryMock();
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var service = new Services.FundoService(fundoRepositoryMock.Object, tipoFundoRepositoryMock.Object, mapper);

            var input = _fixture.GetInput();
            var output = await service.CreateFundo(input);

            fundoRepositoryMock.Verify(
                repository => repository.InsertAsync(input), 
                Times.Once
                );
            output.Fundo.Should().NotBeNull();
            output.Fundo.Codigo.Should().Be(input.Codigo);
            output.Fundo.Nome.Should().Be(input.Nome);
            output.Fundo.Cnpj.Should().Be(input.Cnpj);
            output.Fundo.Patrimonio.Should().Be(input.Patrimonio);
            output.Fundo.CodigoTipo.Should().Be(input.CodigoTipo);

            output.Success.Should().BeTrue();
            output.Errors.Should().BeEmpty();
        }
    }
}
