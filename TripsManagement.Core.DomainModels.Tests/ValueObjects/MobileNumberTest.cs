using System;
using FluentAssertions;
using TripsManagement.Core.DomainModels.ValueObjects;
using Xunit;

namespace TripsManagement.Core.DomainModels.Tests.ValueObjects
{

    public class MobileNumberTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("9103093919")]
        [InlineData("0910309391")]
        [InlineData("091030939198")]
        [InlineData("08136589745")]
        public void MobileNumber_ReturnError(string value)
        {
            // act

            // arrange
            Action comparison = () =>
            {
                var user = new MobileNumber(value);
            };

            // assert
            comparison.Should().Throw<Exception>();
        }

        [Theory]
        [InlineData("+983528962541")]
        [InlineData("+989103093919")]
        public void MobileNumber_ReturnOk(string value)
        {
            // act

            // arrange
            Action comparison = () =>
            {
                var user = new MobileNumber(value);
            };

            // assert
            comparison.Should().NotThrow<Exception>();
        }

    }
}
