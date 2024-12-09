using CebuFitApi.Helpers;
using Xunit;
using System;
using JetBrains.Annotations;

namespace CebuFitApi.UnitTests.Helpers
{
    [TestSubject(typeof(DateTimeExtensions))]
    public class DateTimeExtensionsTest
    {
        [Theory]
        [InlineData("2023-10-11", DayOfWeek.Sunday, "2023-10-08")]
        [InlineData("2023-10-11", DayOfWeek.Monday, "2023-10-09")]
        [InlineData("2023-10-11", DayOfWeek.Tuesday, "2023-10-10")]
        [InlineData("2023-10-11", DayOfWeek.Wednesday, "2023-10-11")]
        [InlineData("2023-10-11", DayOfWeek.Thursday, "2023-10-05")]
        [InlineData("2023-10-11", DayOfWeek.Friday, "2023-10-06")]
        [InlineData("2023-10-11", DayOfWeek.Saturday, "2023-10-07")]
        [InlineData("2023-01-01", DayOfWeek.Sunday, "2023-01-01")] // New Year edge case
        [InlineData("2024-02-29", DayOfWeek.Monday, "2024-02-26")] // Leap year edge case
        [InlineData("2023-12-31", DayOfWeek.Sunday, "2023-12-31")] // End of year edge case
        public void StartOfWeek_ShouldReturnCorrectStartOfWeek(string date, DayOfWeek startOfWeek,
            string expectedStartOfWeek)
        {
            // Arrange
            DateTime dt = DateTime.Parse(date);
            DateTime expected = DateTime.Parse(expectedStartOfWeek);

            // Act
            DateTime result = dt.StartOfWeek(startOfWeek);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}