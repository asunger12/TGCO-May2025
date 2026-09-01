using Xunit;
using Models;

namespace Tests
{
    public class DocumentPostingModelTests
    {
        [Fact]
        public void ValidateGrossAmount_InvalidValues_ThrowsException()
        {
            var model = new DocumentPostingModel { GrossAmount = -1 };
            var context = new ValidationContext(model, null, null);
            var results = Validation.Validate(model, context);
            Assert.Contains("Gross amount must be between 0 and 999,999,999.99", results.First().ErrorMessage);

            model.GrossAmount = 1000000000.00m;
            results = Validation.Validate(model, context);
            Assert.Contains("Gross amount must be between 0 and 999,999,999.99", results.First().ErrorMessage);
        }

        [Fact]
        public void ValidateGrossAmount_ValidValue_PassesValidation()
        {
            var model = new DocumentPostingModel { GrossAmount = 500000000.00m };
            var context = new ValidationContext(model, null, null);
            var results = Validation.Validate(model, context);
            Assert.Empty(results);
        }
    }
}