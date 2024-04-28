using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using Porcupine.Application.Contracts.Models.Users.Dtos;
using Porcupine.Application.Exceptions;
using Porcupine.Application.Services;
using Porcupine.Core.Entities;
using System.Linq.Expressions;
using Xunit;

namespace Porcupine.UnitTests.Services
{
    public class UserServiceTests : BaseServiceTestConfiguration
    {
        private readonly UserAppService _sut;

        public UserServiceTests()
        {
            _sut = new UserAppService(Mapper, UserRepository, UserGroupRepository);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            //Arrange
            var Users = Builder<User>.CreateListOfSize(10).Build().ToList();

            UserRepository.GetAllAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(Users);

            //Act
            var result = await _sut.GetAllListAsync();

            //Assert
            result.Should().HaveCount(10);
            UtilityService.Received().GetUserId();
            await UserRepository.Received().GetAllAsync(Arg.Any<Expression<Func<User, bool>>>());
        }

        [Fact]
        public async Task CreateAsync()
        {
            //Arrange
            var createUser = Builder<CreateUpdateUserDto>.CreateNew().Build();
            var group = Mapper.Map<User>(createUser);
            group.Id = Guid.NewGuid();

            UserRepository.AddAsync(Arg.Any<User>()).Returns(group);

            //Act
            var result = await _sut.CreateAsync(createUser);

            //Assert
            result.Id.Should().Be(group.Id);
            await UserRepository.Received().AddAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_Exception()
        {
            //Arrange
            var updateUser = Builder<CreateUpdateUserDto>.CreateNew().Build();
            var group = Builder<User>.CreateNew().Build();

            UserRepository.GetFirstAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(group);

            //Act
            Func<Task> callUpdateAsync = async () => await _sut.UpdateAsync(Guid.NewGuid(), updateUser);

            //Assert
            await callUpdateAsync.Should().ThrowAsync<BadRequestException>()
                .WithMessage("The selected list does not belong to you");
            await UserRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<User, bool>>>());
            UtilityService.Received().GetUserId();
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Existing_Entity()
        {
            //Arrange
            var updateUser = Builder<CreateUpdateUserDto>.CreateNew().Build();
            var UserId = Guid.NewGuid();
            var group = Builder<User>.CreateNew()
                .With(tl => tl.CreatedBy = new Guid().ToString())
                .With(tl => tl.Id = UserId)
                .Build();

            UserRepository.GetFirstAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(group);
            UserRepository.UpdateAsync(Arg.Any<User>()).Returns(group);

            //Act
            var result = await _sut.UpdateAsync(UserId, updateUser);

            //Assert
            result.Id.Should().Be(UserId);
            await UserRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<User, bool>>>());
            UtilityService.Received().GetUserId();
            await UserRepository.Received().UpdateAsync(Arg.Any<User>());
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Entity()
        {
            //Arrange
            var UserId = Guid.NewGuid();
            var group = Builder<User>.CreateNew()
                .With(tl => tl.CreatedBy = new Guid().ToString())
                .With(tl => tl.Id = UserId)
                .Build();

            UserRepository.GetFirstAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(group);
            UserRepository.DeleteAsync(Arg.Any<User>()).Returns(group);

            //Act
            var result = await _sut.DeleteAsync(Guid.NewGuid());

            //Assert
            result.Id.Should().Be(UserId);
            await UserRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<User, bool>>>());
            await UserRepository.Received().DeleteAsync(Arg.Any<User>());
        }
    }
}