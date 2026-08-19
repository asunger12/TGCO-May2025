using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeOfChallenge.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Sum_ReturnsCorrectSum()
        {
            // Arrange
            var program = new TestProgram();

            // Act
            int result = program.Sum(1, 2);

            // Assert
            Assert.AreEqual(3, result);
        }
    }
}
