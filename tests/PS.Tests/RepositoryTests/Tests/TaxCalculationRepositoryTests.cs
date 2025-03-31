using Moq;
using PS.Application.Abstractions.IRepositories;
using PS.Tests.RepositoryTests.MockRepositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Tests.RepositoryTests.Tests;

public sealed class TaxCalculationRepositoryTests
{
    [Fact]
    public void FindAll_ShouldReturnAllTaxCalculations()
    {
        // Arrange
        var mockData = MockTaxCalculationRepository.GetMockTaxCalculations();
        var mockRepo = new Mock<ITaxCalculationRepository>();
        mockRepo.Setup(repo => repo.FindAll()).Returns(mockData.AsQueryable());

        // Act
        var result = mockRepo.Object.FindAll();

        // Assert
        Assert.Equal(mockData.Count, result.Count());
    }
}
