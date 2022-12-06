using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestApi.App.DataSource;
using RestApi.Controllers;
using RestApi.Resources;

namespace RestApi.UnitTests;

public class CustomersControllerTests
{
    [Fact]
    public async Task Test_Create_StatusCode200()
    {
        var mockedRepository = new Mock<ICustomerService>(MockBehavior.Strict);
        mockedRepository.Setup(x => 
                x.Create(It.IsAny<CustomerResource>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockedRepository.Object);

        var actionResult = await controller.Create(new CustomerResource()) as CreatedResult;

        actionResult!.Location.Should().Contain("customers/1");
        actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Test_Create_StatusCode404()
    {
        var mockRepository = new Mock<ICustomerService>(MockBehavior.Strict);
        mockRepository.Setup(x => 
                x.Create(It.IsAny<CustomerResource>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new ArgumentException());
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockRepository.Object);

        var actionResult = await controller.Create(new CustomerResource()) as BadRequestObjectResult;

        actionResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async Task Test_Create_StatusCode503()
    {
        var mockRepository = new Mock<ICustomerService>();
        mockRepository.Setup(x => 
            x.Create(It.IsAny<CustomerResource>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());
        var controller = new CustomersController(Mock.Of<ILogger<CustomersController>>(), mockRepository.Object);

        var actionResult = await controller.Create(new CustomerResource()) as ObjectResult;

        actionResult!.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
    }
}