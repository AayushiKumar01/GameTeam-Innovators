using System.Linq;

using NUnit.Framework;

using Game.Models;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class CharacterJobEnumHelperTests
    {
        [Test]
        public void CharacterJobEnumHelper_GetListMessageCharacterJob_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnumHelper.GetListMessageCharacterJob;

            // Reset

            // Assert
            Assert.AreEqual(5, result.Count);
        }

    }
}