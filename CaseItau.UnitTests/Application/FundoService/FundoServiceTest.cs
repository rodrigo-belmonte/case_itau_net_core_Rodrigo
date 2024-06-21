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
using CaseItau.Domain.Repository;

namespace CaseItau.UnitTests.Application.FundoService
{
    public class FundoServiceTest: IClassFixture<FundoServiceBaseFixture>
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
            var fundoRepositoryMock = new Mock<IFundoRepository>();
            var tipoFundoRepositoryMock = new Mock<ITipoFundoRepository>();
             tipoFundoRepositoryMock.Setup(r => r.TipoFundoExistsAsync(1)).ReturnsAsync(true);
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            var input = _fixture.GetInput();

            var service = new Services.FundoService(fundoRepositoryMock.Object, tipoFundoRepositoryMock.Object, mapper);

            var output = await service.CreateFundo(input);

            output.Success.Should().BeTrue();
            output.Errors.Should().BeEmpty();
        }
    }
}
