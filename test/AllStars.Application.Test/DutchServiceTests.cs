using AllStars.Application.Services;
using AllStars.Domain.Dutch.Interfaces;
using AllStars.Domain.Dutch.Models;
using AllStars.Domain.Dutch.Models.Commands;
using AllStars.Domain.User.Models;
using FluentAssertions;
using Moq;

namespace AllStars.Application.Test;

public class DutchServiceTests
{
    private readonly Mock<IDutchRepository> _dutchRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly DutchService _dutchService;
    private readonly CancellationToken _token = new CancellationToken();

    public DutchServiceTests()
    {
        _dutchRepositoryMock = new Mock<IDutchRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _dutchService = new DutchService(_dutchRepositoryMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetUserScores_ShouldReturnScores()
    {
        // Arrange
        var userName = "Pawlak";
        var dutchScores = new List<DutchScore>
        {
            new() { Id = Guid.NewGuid(), Points = 100 }
        };

        _dutchRepositoryMock
            .Setup(repo => repo.GetUserScores(userName, _token))
            .ReturnsAsync(dutchScores);

        // Act
        var result = await _dutchService.GetUserScores(userName, _token);

        // Assert
        result.Should().NotBeNull();
        result.First().Should().Be(dutchScores.First());
        _dutchRepositoryMock.Verify(repo => repo.GetUserScores(userName, _token), Times.Once);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllScores()
    {
        // Arrange
        var allScores = new List<DutchScore>
        {
            new() { Id = Guid.NewGuid(), Points = 200 },
            new() { Id = Guid.NewGuid(), Points = 300 }
        };

        _dutchRepositoryMock
            .Setup(repo => repo.GetAll(_token))
            .ReturnsAsync(allScores);

        // Act
        var result = await _dutchService.GetAll(_token);

        // Assert
        result.Should().NotBeNull();
        result.First().Should().Be(allScores.First());
        result.Skip(1).First().Should().Be(allScores.Skip(1).First());
        _dutchRepositoryMock.Verify(repo => repo.GetAll(_token), Times.Once);
    }

    [Fact]
    public async Task UpdateOne_ShouldUpdateScore_WhenScoreExists()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var nickName = "JohnDoe";
        var points = 150; 

        var existingScore = new DutchScore { Id = Guid.NewGuid(), Points = 100 };

        _dutchRepositoryMock
            .Setup(repo => repo.GetOneScore(gameId, nickName, _token))
            .ReturnsAsync(existingScore);

        // Act
        var result = await _dutchService.UpdateOne(gameId, nickName, points, _token);

        // Assert
        Assert.True(result);
        _dutchRepositoryMock.Verify(repo => repo.GetOneScore(gameId, nickName, _token), Times.Once);
        _dutchRepositoryMock.Verify(repo => repo.UpdateOne(existingScore, points, _token), Times.Once);
    }

    [Fact]
    public async Task UpdateOne_ShouldReturnFalse_WhenScoreDoesNotExist()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var nickName = "Unknown User";
        var points = 150;

        _dutchRepositoryMock
            .Setup(repo => repo.GetOneScore(gameId, nickName, _token))
            .ReturnsAsync(null as DutchScore);

        // Act
        var result = await _dutchService.UpdateOne(gameId, nickName, points, _token);

        // Assert
        Assert.False(result);
        _dutchRepositoryMock.Verify(repo => repo.GetOneScore(gameId, nickName, _token), Times.Once);
        _dutchRepositoryMock.Verify(repo => repo.UpdateOne(It.IsAny<DutchScore>(), It.IsAny<int>(), _token), Times.Never);
    }

    [Fact]
    public async Task CreateMany_ShouldCreateGame_WhenAllUsersExist()
    {
        // Arrange
        var command = new CreateDutchGameCommand
        {
            Comment = "New Game",
            ScorePairs =
            [
                new ScorePair { NickName = "User1", Score = 50 },
                new ScorePair { NickName = "User2", Score = 100 }
            ]
        };
        

        var users = new List<AllStarUser>
        {
            new() { Id = Guid.NewGuid(), Nickname = "User1" },
            new() { Id = Guid.NewGuid(), Nickname = "User2" }
        };

        _userRepositoryMock
            .Setup(repo => repo.GetManyAsync(It.IsAny<IEnumerable<string>>(), _token))
            .ReturnsAsync(users);

        // Act
        await _dutchService.CreateMany(command, _token);

        // Assert
        _dutchRepositoryMock.Verify(repo => repo.CreateMany(It.IsAny<DutchGame>(), It.IsAny<IEnumerable<DutchScore>>(), _token), Times.Once);
    }

    [Fact]
    public async Task CreateMany_ShouldThrowException_WhenSomeUsersAreMissing()
    {
        // Arrange
        var command = new CreateDutchGameCommand
        {
            Comment = "New Game",
            ScorePairs =
            [
                new() { NickName = "User A", Score = 50 },
                new() { NickName = "User B", Score = 100 }
            ]
        };
        
        var users = new List<AllStarUser>
        {
            new() { Id = Guid.NewGuid(), Nickname = "User A" }
        };

        _userRepositoryMock
            .Setup(repo => repo.GetManyAsync(It.IsAny<IEnumerable<string>>(), _token))
            .ReturnsAsync(users);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _dutchService.CreateMany(command, _token));
    }


}
