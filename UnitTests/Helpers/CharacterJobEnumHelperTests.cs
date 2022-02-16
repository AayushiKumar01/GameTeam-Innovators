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

        [Test]
        public void CharacterJobEnumHelper_ConvertMessageToEnum_Valid_Head_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnumHelper.ConvertMessageToEnum("Frigate");

            // Reset

            // Assert
            Assert.AreEqual(CharacterJobEnum.Frigate, result);
        }

        [Test]
        public void CharacterJobEnumHelper_ConvertMessageToEnum_InValid_Bogus_Should_Fail()
        {
            // Arrange

            // Act
            var result = CharacterJobEnumHelper.ConvertMessageToEnum("bogus");

            // Reset

            // Assert
            Assert.AreEqual(CharacterJobEnum.Unknown, result);
        }

    }
}