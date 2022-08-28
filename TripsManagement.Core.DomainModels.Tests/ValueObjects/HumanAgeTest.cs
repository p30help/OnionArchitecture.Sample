using System;
using FluentAssertions;
using TripsManagement.Core.DomainModels.ValueObjects;
using Xunit;

namespace TripsManagement.Core.DomainModels.Tests.ValueObjects
{
    public class HumanAgeTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(200)]
        [InlineData(5000)]
        [InlineData(-10)]
        public void HumanAge_ReturnError(short value)
        {
            // act

            // arrange
            Action comparison = () =>
            {
                var user = new HumanAgeField(value);
            };

            // assert
            comparison.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void HumanAge_ReturnOk(short value)
        {
            // act

            // arrange
            Action comparison = () =>
            {
                var user = new HumanAgeField(value);
            };

            // assert
            comparison.Should().NotThrow<Exception>();
        }

    }
}
