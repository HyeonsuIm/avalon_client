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

namespace Avalron
{
    class Music
    {
        private SoundPlayer player = new SoundPlayer();

        public Music(string MusicPath)
        {
            player.SoundLocation = Application.StartupPath + MusicPath;
        }

        public Music()
        {
            this.player.SoundLocation = Application.StartupPath + @"\bgm\모두의 마블.wav";
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
