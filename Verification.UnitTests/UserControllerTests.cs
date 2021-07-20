using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Verification.Api.Controllers;
using Verification.Api.Data.IRepos;
using Verification.Api.Dtos;
using Verification.Api.Models;
using Xunit;
using Xunit.Abstractions;


namespace Verification.UnitTests
{
    public class UserControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Mock<IUserRepo> _repositoryStub = new();
        private readonly Mock<ILogger<UserController>> _loggerStub = new();
        private readonly Mock<IMapper> _mapper = new();
        private readonly Random _rand = new();

        public UserControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task CreateCustomerByEmail_WithUnitTests_ReturnsOk()
        {
            // Arrange
            var expectedUserCreateDto = CreateRandomUser();
            _repositoryStub.Setup(repo => repo.Create(It.IsAny<User>()))
                .ReturnsAsync(expectedUserCreateDto);
            var controller = new UserController(_repositoryStub.Object, _mapper.Object, _loggerStub.Object);

            // Act

            var result = await controller.CreateCustomerByEmail(new UserCreateDto
            {
                FullName = Guid.NewGuid().ToString(),
                VCode = _rand.Next(10000, 100000).ToString(),
                Phone = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid() + "@test.com"
            });

            // Assert
            var createdItem = result;
            _testOutputHelper.WriteLine(createdItem.ToString());

        }

        private User CreateRandomUser()
        {
            return new()
            {
                Id = _rand.Next(10000),
                FullName = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid() + "@test.com"
            };
        }
    }
}
