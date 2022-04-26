using System;
using Xunit;

namespace StringHelpers.Tests
{
    public class StringHelperTests
    {
        #region ToFirstLetterUpper

        [Fact]
        public void ToFirstLetterUpper_EnglishLower_ReturnsFirstLetterUpper()
        {
            //Arrange
            string word = "program";
            string expectedResult = "Program";
            //Act
            string result = word.ToFirstLetterUpper();
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ToFirstLetterUpper_RussianLower_ReturnsFirstLetterUpper()
        {
            string word = "слово";
            string result = word.ToFirstLetterUpper();
            Assert.Equal("Слово", result);
        }

        [Fact]
        public void ToFirstLetterUpper_FromUpper_ReturnsFirstLetterUpper()
        {
            string word = "HELLO, WORLD!";
            string result = word.ToFirstLetterUpper();
            Assert.Equal("Hello, world!", result);
        }

        [Fact]
        public void ToFirstLetterUpper_FromMixedCase_ReturnsFirstLetterUpper()
        {
            string word = "mIxEd CaSe";
            string result = word.ToFirstLetterUpper();
            Assert.Equal("Mixed case", result);
        }

        [Fact]
        public void ToFirstLetterUpper_FirstLetterUpper_ReturnsUnchanged()
        {
            string word = "Test";
            string result = word.ToFirstLetterUpper();
            Assert.Equal(word, result);
        }

        [Fact]
        public void ToFirstLetterUpper_Numeric_ReturnsUnchanged()
        {
            string word = "12345";
            string result = word.ToFirstLetterUpper();
            Assert.Equal("12345", result);
        }

        [Fact]
        public void ToFirstLetterUpper_Empty_ReturnsEmpty()
        {
            string word = "";
            string result = word.ToFirstLetterUpper();
            Assert.Equal("", result);
        }
        #endregion

        #region IsNumeric
        [Theory]
        [InlineData("12345")]
        [InlineData("1 000 000")]
        [InlineData("0")]
        [InlineData("-67890")]
        [InlineData("12,5")]
        [InlineData("-0,6")]
        [InlineData(",74")]
        [InlineData("1e+7")]
        [InlineData("1e+1024")]
        [InlineData("-1234567890123456789012345678901234567890e+1024")]
        [InlineData("NaN")]
        [InlineData("-Inf")]
        [InlineData("Inf")]
        [InlineData("1e-1024")]
        public void IsNumeric_Numbers_ReturnsTrue(string number)
        {
            bool result = number.IsNumeric();
            Assert.True(result);
        }

        [Theory]
        [InlineData("abcd")]
        [InlineData("12e")]
        [InlineData("1.5")]
        [InlineData("1_456")]
        [InlineData("-Infinity")]
        [InlineData("Infinity")]
        public void IsNumeric_Numbers_ReturnsFalse(string notNumber)
        {
            bool result = notNumber.IsNumeric();
            Assert.False(result);
        }

        [Fact]
        public void IsNumeric_Empty_ReturnsFalse()
        {
            bool result = string.Empty.IsNumeric(); //""
            Assert.False(result);
        }

        [Fact]
        public void IsNumeric_Null_ReturnsFalse()
        {
            string nullString = null;
            bool result = nullString.IsNumeric();
            Assert.False(result);
        }

        #endregion
    }
}
