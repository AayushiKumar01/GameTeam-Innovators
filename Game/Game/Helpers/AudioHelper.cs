using Plugin.SimpleAudioPlayer;

namespace Game.Helpers
{
    /// <summary>
    /// Class for helping load and play audio with error checking
    /// </summary>
    public class AudioHelper
    {
        
        //hold audio file
        public ISimpleAudioPlayer BattleStartAudio;
        public ISimpleAudioPlayer GameOverAudio;

        public AudioHelper()
        {
            //Initialzing and loading audio file
            BattleStartAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            GameOverAudio = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();   
            if (BattleStartAudio != null)
            {
                BattleStartAudio.Load("autobattle.mp3");

            }
            if (GameOverAudio != null)
            {
                GameOverAudio.Load("gameover.mp3");
            }
        }

        /// <summary>
        /// Play Battle Start audio only if its already loaded to prevent error
        /// </summary>
        public void playBattleStart()
        {
            if (BattleStartAudio != null)
            {
                BattleStartAudio.Play();
            }
        }

        /// <summary>
        /// Stop Battle audio only if its already loaded to prevent error
        /// </summary>
        public void stopStartAudio()
        {
            if (BattleStartAudio != null)
            {
                BattleStartAudio.Stop();
            }
        }

        /// <summary>
        /// Play Game Over audio only if its already loaded to prevent error
        /// </summary>
        public void playGameOverAudio()
        {
            if (GameOverAudio != null)
            {
                GameOverAudio.Play();
            }
        }
    }
    
}