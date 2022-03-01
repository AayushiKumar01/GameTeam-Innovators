using Plugin.SimpleAudioPlayer;

namespace Game.Helpers
{

    
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

        public void playBattleStart()
        {
            if (BattleStartAudio != null)
            {
                BattleStartAudio.Play();
            }
        }

        public void stopStartAudio()
        {
            if (BattleStartAudio != null)
            {
                BattleStartAudio.Stop();
            }
        }

        public void playGameOverAudio()
        {
            if (GameOverAudio != null)
            {
                GameOverAudio.Play();
            }
        }
    }
    
}