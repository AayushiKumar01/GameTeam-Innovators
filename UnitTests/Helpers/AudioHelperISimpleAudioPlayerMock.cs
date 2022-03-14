using System;
using System.IO;
using Plugin.SimpleAudioPlayer;

namespace UnitTests.Helpers
{
    public class AudioHelperISimpleAudioPlayerMock : ISimpleAudioPlayer
    {
        public void Dispose()
        {
        }

        public bool Load(Stream audioStream)
        {
            return true;
        }

        public bool Load(string fileName)
        {
            return true;
        }

        public void Play()
        {
        }

        public void Pause()
        {
        }

        public void Stop()
        {
        }

        public void Seek(double position)
        {
        }

        public double Duration { get; }
        public double CurrentPosition { get; }
        public double Volume { get; set; }
        public double Balance { get; set; }
        public bool IsPlaying { get; }
        public bool Loop { get; set; }
        public bool CanSeek { get; }
        public event EventHandler PlaybackEnded;
    }
}