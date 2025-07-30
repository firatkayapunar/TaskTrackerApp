using FluentAssertions;
using Moq;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.CQRS.Users.Commands.Request;
using TaskTrackerApp.CQRS.Users.Handlers.CommandHandlers;
using TaskTrackerApp.Domain.Entities;

namespace TaskTrackerApp.Tests.CQRS.Users;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<IJwtTokenService> _jwtTokenServiceMock = new();

    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _handler = new LoginUserCommandHandler(
        _userRepositoryMock.Object,
        _jwtTokenServiceMock.Object,
         _passwordHasherMock.Object
        );
    }

    [Fact(DisplayName = "Handle() should return token when credentials are valid")]
    public async Task Handle_ValidCredentials_ReturnsSuccessWithToken()
    {
        var user = new User
        {
            Email = "firat@example.com",
            PasswordHash = "bcrypt-hash"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync("firat@example.com"))
            .ReturnsAsync(user);

        _passwordHasherMock
            .Setup(h => h.Verify("12345", "bcrypt-hash"))
            .Returns(true);

        _jwtTokenServiceMock
            .Setup(j => j.GenerateToken(user))
            .Returns("mocked-jwt-token");

        var request = new LoginUserCommandRequest("firat@example.com", "12345");

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue("the login should succeed when valid credentials are provided");
        result.Data.Should().NotBeNull("a successful login should return a response containing token data");
        result.Data!.Token.Should().Be("mocked-jwt-token", "the expected token should match the mocked JWT token value");
    }
}
