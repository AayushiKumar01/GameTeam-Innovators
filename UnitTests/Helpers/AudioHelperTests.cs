using System.IO;
using Game.Helpers;
using NUnit.Framework;

namespace UnitTests.Helpers
{
    [TestFixture]
    class AudioHelperTests
    {

        AudioHelper audio;

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
        
        [Test]
        public void AudioHelper_With_Default_Constructor_Should_Pass()
        {
            // Arrange
            audio = new AudioHelper();
            
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
        public void AudioHelper_Mock_Methods_Should_Pass()
        {
            // Arrange
            AudioHelperISimpleAudioPlayerMock mock = new AudioHelperISimpleAudioPlayerMock();
            mock.Dispose();
            mock.Load("");
            mock.Load(new MemoryStream());
            mock.Pause();
            mock.Seek(0.0);
            mock.Stop();
            

            // Act
            mock.Play();

            // Reset
            mock = null;
            
            // Assert
            Assert.True(true); // If got here we passed
        }

        
        [Test]
        public void AudioHelper_Mock_Get_Set_Should_Pass()
        {
            // Arrange
            AudioHelperISimpleAudioPlayerMock mock = new AudioHelperISimpleAudioPlayerMock();
            double bal = mock.Balance;
            double dur = mock.Duration;
            double vol = mock.Volume;
            bool canseek = mock.CanSeek;
            double pos = mock.CurrentPosition;
            bool playing = mock.IsPlaying;
            bool loop = mock.Loop;
            
            // Act

            mock.Balance = bal;
            mock.Volume = vol;
            mock.Loop = loop;
            
            // Reset
            mock = null;
            
            // Assert
            Assert.True(true); // If got here we passed
        }
        
    }

}

