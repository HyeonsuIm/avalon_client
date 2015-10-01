using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 아래로 추가된 include
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Reflection;

namespace Avalron
{
    class Music
    {
        private SoundPlayer player = null;

        public Music(string MusicPath)
        {
            player = new SoundPlayer();
            try {
                player.SoundLocation = Application.StartupPath + MusicPath;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Music()
        {
            System.IO.Stream sound = Properties.Resources.Title_Theme;
            player = new SoundPlayer(sound);
        }

        public void Play()
        {
            try
            {
                this.player.PlayLooping();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "음악 제생에 에러가 발생했습니다.");
            }
        }

        public void Stop()
        {
            this.player.Stop();
        }


    }
}
