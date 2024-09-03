using PasswordFeedbackManager.Models;
using PasswordFeedbackManager.Services;

namespace Tests
{
    public class PasswordFeedbackServiceTests
    {
        private readonly IPasswordFeedbackService _service;

        public PasswordFeedbackServiceTests()
        {
            _service = new PasswordFeedbackService();
        }

        [Fact]
        public void Add_ShouldAddPasswordFeedback()
        {
            // Arrange
            var passwordFeedback = new PasswordFeedbackModel { Password = "testPassword", Feedback = "testFeedback" };

            // Act
            _service.AddCombination(passwordFeedback);

            // Assert
            Assert.Equal(1, _service.GetCombinationsCount());
        }

        [Fact]
        public void GetFeedback_ShouldReturnCorrectFeedback()
        {
            // Arrange
            var passwordFeedback = new PasswordFeedbackModel { Password = "testPassword", Feedback = "testFeedback" };
            _service.AddCombination(passwordFeedback);

            // Act
            var result = _service.GetFeedback("testPassword");

            // Assert
            Assert.Equal("t e s t F e e d b a c k", result);
        }

        [Fact]
        public void GetFeedback_ShouldReturnNullForUnknownPassword()
        {
            // Act
            var result = _service.GetFeedback("unknownPassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetPasswordHash_ShouldReturnCorrectHash()
        {
            // Arrange
            var passwordFeedback = new PasswordFeedbackModel { Password = "testPassword", Feedback = "testFeedback" };
            _service.AddCombination(passwordFeedback);

            // Act
            var result = _service.GetPasswordHash("testFeedback");

            // Assert
            Assert.Equal("fd5cb51bafd60f6fdbedde6e62c473da6f247db271633e15919bab78a02ee9eb", result);
        }

        [Fact]
        public void GetPassword_ShouldReturnNullForUnknownFeedback()
        {
            // Act
            var result = _service.GetPasswordHash("unknownFeedback");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCombinationsCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var passwordFeedback1 = new PasswordFeedbackModel { Password = "testPassword1", Feedback = "testFeedback1" };
            var passwordFeedback2 = new PasswordFeedbackModel { Password = "testPassword2", Feedback = "testFeedback2" };
            _service.AddCombination(passwordFeedback1);
            _service.AddCombination(passwordFeedback2);

            // Act
            var result = _service.GetCombinationsCount();

            // Assert
            Assert.Equal(2, result);
        }
    }
}