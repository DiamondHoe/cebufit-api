using CebuFitApi.Helpers;
using JetBrains.Annotations;
using Xunit;

namespace CebuFitApi.UnitTests.Helpers;

[TestSubject(typeof(PasswordGenerator))]
public class PasswordGeneratorTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void GenerateRandomPassword_ShouldReturnPasswordOfSpecifiedLength(int length)
    {
        // Act
        string password = PasswordGenerator.GenerateRandomPassword(length);

        // Assert
        Assert.Equal(length, password.Length);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(100)]
    public void GenerateRandomPassword_ShouldContainValidCharactersOnly(int length)
    {
        // Arrange
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";

        // Act
        string password = PasswordGenerator.GenerateRandomPassword(length);

        // Assert
        foreach (char c in password)
        {
            Assert.Contains(c, validChars);
        }
    }

    [Fact]
    public void GenerateRandomPassword_ShouldReturnEmptyString_WhenLengthIsZero()
    {
        // Act
        string password = PasswordGenerator.GenerateRandomPassword(0);

        // Assert
        Assert.Empty(password);
    }

    [Fact]
    public void GenerateRandomPassword_ShouldReturnEmptyString_WhenLengthIsNegative()
    {
        // Arrange
        int negativeLength = -1;

        // Act & Assert
        string password = PasswordGenerator.GenerateRandomPassword(negativeLength);
        Assert.Empty(password);
    }
}