using Application.Commands.GenerateOtp;
using Application.Repositories;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;

namespace Application.UnitTests;

[TestClass]
public class GenerateOtpCommandHandlerTests
{
    private GenerateOtpCommandHandler _sut;
    private Mock<IOtpRepository> _otpRepositoryMock;
    private OtpConfiguration _otpConfiguration;
    private Fixture _fixture;

    public GenerateOtpCommandHandlerTests()
    {
        _fixture = new Fixture();
        _otpRepositoryMock = new Mock<IOtpRepository>();
        _otpConfiguration = new OtpConfiguration
        {
            Validity = 30
        };

        _sut = new GenerateOtpCommandHandler(_otpRepositoryMock.Object, _otpConfiguration);
    }

    [TestMethod]
    public async Task ProcessCommandAsync_ForUserId_GeneratesOtp()
    {
        // Arrange
        GenerateOtpCommand command = _fixture.Create<GenerateOtpCommand>();

        // Act
        Otp otp = await _sut.ProcessCommandAsync(command);

        // Assert
        Assert.IsNotNull(otp);
        Assert.AreEqual(command.UserId, otp.UserId);
        Assert.AreEqual(_otpConfiguration.Validity, otp.Validity);
        Assert.IsTrue(otp.Code > 100000);
        Assert.IsTrue(otp.Code < 999999);

        _otpRepositoryMock.Verify(x => x.CreateAsync(otp), Times.Once);
    }
}
