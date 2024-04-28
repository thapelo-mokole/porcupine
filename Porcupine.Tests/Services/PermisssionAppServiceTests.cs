using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using Porcupine.Application.Contracts.Models.Permissions.Dtos;
using Porcupine.Application.Services;
using Porcupine.Core.Entities;
using System.Linq.Expressions;
using Xunit;

namespace Porcupine.UnitTests.Services
{
    public class PermisssionAppServiceTests : BaseServiceTestConfiguration
    {
        private readonly PermissionAppService _sut;

        public PermisssionAppServiceTests()
        {
            _sut = new PermissionAppService(Mapper, PermissionRepository);
        }

        [Fact]
        public async Task GetAllByListIdAsync()
        {
            // Arrange
            var permissions = Builder<Permission>.CreateListOfSize(10).Build().ToList();

            PermissionRepository.GetAllAsync(Arg.Any<Expression<Func<Permission, bool>>>()).Returns(permissions);

            // Act
            var result = await _sut.GetAllListAsync();

            // Assert
            result.Should().NotBeNull();
            await PermissionRepository.Received().GetAllAsync(Arg.Any<Expression<Func<Permission, bool>>>());
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var createPermission = Builder<CreateUpdatePermissionDto>.CreateNew().Build();
            var permission = Builder<Permission>.CreateNew().With(ti => ti.Id = Guid.NewGuid()).Build();

            PermissionRepository.AddAsync(Arg.Any<Permission>()).Returns(permission);

            // Act
            var result = await _sut.CreateAsync(createPermission);

            // Assert
            result.Id.Should().Be(permission.Id);
            await PermissionRepository.Received().AddAsync(Arg.Any<Permission>());
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            var permission = Builder<Permission>.CreateNew().With(c => c.Id = Guid.NewGuid()).Build();
            PermissionRepository.GetFirstAsync(Arg.Any<Expression<Func<Permission, bool>>>()).Returns(permission);
            PermissionRepository.DeleteAsync(Arg.Any<Permission>()).Returns(permission);

            // Act
            var result = await _sut.DeleteAsync(Guid.NewGuid());

            // Assert
            result.Id.Should().Be(permission.Id);
            await PermissionRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Permission, bool>>>());
            await PermissionRepository.Received().DeleteAsync(Arg.Any<Permission>());
        }
    }
}