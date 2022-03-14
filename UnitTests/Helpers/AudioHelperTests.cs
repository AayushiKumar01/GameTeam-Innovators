using Game.Helpers;
using NUnit.Framework;

namespace UnitTests.Helpers
{
    [TestFixture]
    class AudioHelperTests
    {

        AudioHelper audio;

        [SetUp]
        public void Setup()
        {
            audio = new AudioHelper(null);
        }

        [TearDown]
        public void TearDown()
        {
            audio = null;
        }

        [Test]
        public void AudioHelper_With_Null_Player_Should_Pass()
        {
            // Arrange
            audio = new AudioHelper(null);
            
            // Act
            audio.playBattleStart();
            audio.stopStartAudio();
            audio.playGameOverAudio();

            // Reset
            audio = null;
            
            // Assert
            Assert.True(true); // If got here we passed
        }
        
        [Test]
        public void AudioHelper_With_Player_Should_Pass()
        {
            // Arrange
            audio = new AudioHelper(new AudioHelperISimpleAudioPlayerMock());
            
            // Act
            audio.playBattleStart();
            audio.stopStartAudio();
            audio.playGameOverAudio();

            // Reset
            audio = null;
            
            // Assert
            Assert.True(true); // If got here we passed
        }

    }

}

