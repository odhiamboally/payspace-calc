using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Moq;
using PS.Application.Abstractions.ICaching;
using PS.Application.Abstractions.Interfaces;
using PS.Application.Abstractions.IRepositories;
using PS.Application.Abstractions.IServices;
using PS.Domain.Entities;
using PS.Infrastructure.Implementations.Services;
using PS.Tests.RepositoryTests.MockRepositories;
using PS.Tests.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Tests.ServiceTests.MockServices;

internal sealed class MockTaxCalculationService
{
    public static ITaxCalculationService GetMockService()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockMapper = new Mock<IMapper>();
        var mockCacheService = new Mock<ICacheService>();

        // Mock repositories
        var mockTaxCalculationRepository = new Mock<ITaxCalculationRepository>();
        var taxCalculations = MockTaxCalculationRepository.GetMockTaxCalculations().AsQueryable();
        mockTaxCalculationRepository
        .Setup(repo => repo.FindAll())
            .Returns(new TestAsyncEnumerable<TaxCalculation>(taxCalculations));

        var mockCountryRepository = new Mock<ICountryRepository>();
        var countries = MockCountryRepository.GetMockCountries().AsQueryable();
        mockCountryRepository
            .Setup(repo => repo.FindWithRelatedDatasAsync(It.IsAny<List<int>>()))
            .Returns(new TestAsyncEnumerable<Country>(countries));

        // Set up UnitOfWork
        mockUnitOfWork.Setup(uow => uow.TaxCalculationRepository).Returns(mockTaxCalculationRepository.Object);
        mockUnitOfWork.Setup(uow => uow.CountryRepository).Returns(mockCountryRepository.Object);

        return new TaxCalculationService(mockUnitOfWork.Object, mockMapper.Object, mockCacheService.Object);
    }
}
