using System.Media;

namespace SimpleChessApp.Extras
{
    public static class PlaySound
    {
        static SoundPlayer wmp;

        static PlaySound()
        {
            wmp = new SoundPlayer(Properties.Resources.ms);
            wmp.LoadTimeout = 0;
        }

        public static void Play()
        {
            wmp.Play();
        }
    }
}
