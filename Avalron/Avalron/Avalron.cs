using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public partial class Avalron : Form
    {
        static public GameClient gameClient;
        Person[] person = new Person[10];
        VoteTrack voteTrack = new VoteTrack(5);
        RoundTrack roundTrack = new RoundTrack(5);
        Chatting chatting;
        String ServerAddress = "203.255.3.72";
        Thread GetClient;
        public static bool closing = false;

        public enum PersonCard
        {
            Merlin, Assassin, Percival, Mordred, Morgana, Oberon,
            ArtherServant1, Artherservant2, Artherservant3, Artherservant4, Artherservant5,
            MordredMiniion1, MordredMiniion2, MordredMiniion3
        };

        bool isServer = true;

        public Avalron()
        {
            InitializeComponent();
            for (int i = 0; i < person.Length; i++)
            {
                person[i] = new Person(this.Controls, i);
            }
            chatting = new Chatting(Controls);

            if (isServer)
            {
                Server server = new Server();
            }

            gameClient = new GameClient(ServerAddress);

            voteTrack.SetPosition(new Point(30, 100));
            voteTrack.SetCollection(this.Controls);
            voteTrack.Next();

            roundTrack.SetPosition(new Point(400, 100));
            roundTrack.SetCollection(this.Controls);
            roundTrack.SetResult(true);
            roundTrack.SetResult(false);

            GetClient = new Thread(new ThreadStart(chatting.RunGetChat));
            GetClient.Start();
        }

        ~Avalron()
        {
            gameClient.Close();
            if (GetClient.IsAlive)
                GetClient.Abort();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            closing = true;
            GetClient.Join();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vote vote = new Vote();
            vote.StartPosition = FormStartPosition.CenterParent;
            vote.ShowDialog();

            MessageBoxEx.Show(this, vote.getResult().ToString());
        }
    }
}
