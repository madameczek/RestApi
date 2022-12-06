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
        var mockRepository = new Mock<ICustomerService>();
        mockRepository.Setup(x => x.Create(new CustomerResource(), CancellationToken.None))
            .ReturnsAsync(1);
        var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, mockRepository.Object);

        var actionResult = await controller.Create(new Mock<CustomerResource>().Object) as CreatedResult;

        actionResult!.Location.Should().Contain("customers/1");
        actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Test_Create_StatusCode404()
    {
        var mockRepository = new Mock<ICustomerService>();
        mockRepository.Setup(x => x.Create(new Mock<CustomerResource>().Object, CancellationToken.None))
            .Throws(new ArgumentException());
        var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, mockRepository.Object);

        var actionResult = await controller.Create(new Mock<CustomerResource>().Object) as BadRequestResult;

        actionResult!.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async Task Test_Create_StatusCode503()
    {
        var mockRepository = new Mock<ICustomerService>();
        mockRepository.Setup(x => x.Create(new Mock<CustomerResource>().Object, CancellationToken.None))
            .Throws(new Exception());
        var controller = new CustomersController(new Mock<ILogger<CustomersController>>().Object, mockRepository.Object);

        var actionResult = await controller.Create(new Mock<CustomerResource>().Object) as StatusCodeResult;

        actionResult!.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
    }
}