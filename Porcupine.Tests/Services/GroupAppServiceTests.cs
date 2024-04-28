using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using Porcupine.Application.Exceptions;
using Porcupine.Application.Services;
using System.Linq.Expressions;
using Xunit;
using Porcupine.Core.Entities;
using Porcupine.Application.Contracts.Models.Groups.Dtos;

namespace Porcupine.UnitTests.Services
{
    public class GroupAppServiceTests : BaseServiceTestConfiguration
    {
        private readonly GroupAppService _sut;

        public GroupAppServiceTests()
        {
            _sut = new GroupAppService(Mapper, GroupRepository, GroupPermissionRepository);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            //Arrange
            var Groups = Builder<Group>.CreateListOfSize(10).Build().ToList();

            GroupRepository.GetAllAsync(Arg.Any<Expression<Func<Group, bool>>>()).Returns(Groups);

            //Act
            var result = await _sut.GetAllListAsync();

            //Assert
            result.Should().HaveCount(10);
            UtilityService.Received().GetUserId();
            await GroupRepository.Received().GetAllAsync(Arg.Any<Expression<Func<Group, bool>>>());
        }

        [Fact]
        public async Task CreateAsync()
        {
            //Arrange
            var createGroup = Builder<CreateUpdateGroupDto>.CreateNew().Build();
            var group = Mapper.Map<Group>(createGroup);
            group.Id = Guid.NewGuid();

            GroupRepository.AddAsync(Arg.Any<Group>()).Returns(group);

            //Act
            var result = await _sut.CreateAsync(createGroup);

            //Assert
            result.Id.Should().Be(group.Id);
            await GroupRepository.Received().AddAsync(Arg.Any<Group>());
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_Exception()
        {
            //Arrange
            var updateGroup = Builder<CreateUpdateGroupDto>.CreateNew().Build();
            var group = Builder<Group>.CreateNew().Build();

            GroupRepository.GetFirstAsync(Arg.Any<Expression<Func<Group, bool>>>()).Returns(group);

            //Act
            Func<Task> callUpdateAsync = async () => await _sut.UpdateAsync(Guid.NewGuid(), updateGroup);

            //Assert
            await callUpdateAsync.Should().ThrowAsync<BadRequestException>()
                .WithMessage("The selected list does not belong to you");
            await GroupRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Group, bool>>>());
            UtilityService.Received().GetUserId();
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Existing()
        {
            //Arrange
            var updateGroup = Builder<CreateUpdateGroupDto>.CreateNew().Build();
            var GroupId = Guid.NewGuid();
            var group = Builder<Group>.CreateNew()
                .With(tl => tl.CreatedBy = new Guid().ToString())
                .With(tl => tl.Id = GroupId)
                .Build();

            GroupRepository.GetFirstAsync(Arg.Any<Expression<Func<Group, bool>>>()).Returns(group);
            GroupRepository.UpdateAsync(Arg.Any<Group>()).Returns(group);

            //Act
            var result = await _sut.UpdateAsync(GroupId, updateGroup);

            //Assert
            result.Id.Should().Be(GroupId);
            await GroupRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Group, bool>>>());
            UtilityService.Received().GetUserId();
            await GroupRepository.Received().UpdateAsync(Arg.Any<Group>());
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete()
        {
            //Arrange
            var GroupId = Guid.NewGuid();
            var group = Builder<Group>.CreateNew()
                .With(tl => tl.CreatedBy = new Guid().ToString())
                .With(tl => tl.Id = GroupId)
                .Build();

            GroupRepository.GetFirstAsync(Arg.Any<Expression<Func<Group, bool>>>()).Returns(group);
            GroupRepository.DeleteAsync(Arg.Any<Group>()).Returns(group);

            //Act
            var result = await _sut.DeleteAsync(Guid.NewGuid());

            //Assert
            result.Id.Should().Be(GroupId);
            await GroupRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Group, bool>>>());
            await GroupRepository.Received().DeleteAsync(Arg.Any<Group>());
        }
    }
}