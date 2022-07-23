using Application.Commands.CheckOtp;
using Application.Repositories;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace Application.UnitTests;

[TestClass]
public class CheckOtpCommandHandlerTests
{
    private CheckOtpCommandHandler _sut;
    private Mock<IOtpRepository> _otpRepositoryMock;
    private OtpConfiguration _otpConfiguration;
    private Fixture _fixture;

    public CheckOtpCommandHandlerTests()
    {
        _fixture = new Fixture();
        _otpRepositoryMock = new Mock<IOtpRepository>();
        _otpConfiguration = new OtpConfiguration
        {
            Validity = 30
        };

        _sut = new CheckOtpCommandHandler(_otpRepositoryMock.Object, _otpConfiguration);
    }

    [TestMethod]
    public async Task ProcessCommandAsync_ExpiredToken_ReturnFalse()
    {
        // Arrange
        CheckOtpCommand command = _fixture.Create<CheckOtpCommand>();

        Otp otp = _fixture.Build<Otp>()
            .With(x => x.UserId, command.UserId)
            .With(x => x.Code, command.Code)
            .With(x => x.Timestamp, DateTime.UtcNow.AddSeconds(-30))
            .Create();

        _otpRepositoryMock
            .Setup(x => x.GetAsync(command.UserId, command.Code))
            .ReturnsAsync(otp)
            .Verifiable();

        // Act
        bool result = await _sut.ProcessCommandAsync(command);

        // Assert
        Assert.IsFalse(result);

        _otpRepositoryMock.Verify();
    }

    [TestMethod]
    public async Task ProcessCommandAsync_ValidToken_ReturnTrue()
    {
        // Arrange
        CheckOtpCommand command = _fixture.Create<CheckOtpCommand>();

        Otp otp = _fixture.Build<Otp>()
            .With(x => x.UserId, command.UserId)
            .With(x => x.Code, command.Code)
            .With(x => x.Timestamp, DateTime.UtcNow)
            .Create();

        _otpRepositoryMock
            .Setup(x => x.GetAsync(command.UserId, command.Code))
            .ReturnsAsync(otp)
            .Verifiable();

        // Act
        bool result = await _sut.ProcessCommandAsync(command);

        // Assert
        Assert.IsTrue(result);

        _otpRepositoryMock.Verify();
        _otpRepositoryMock.Verify(x => x.InvalidateAsync(otp.UserId, otp.Code), Times.Once);
    }
}
