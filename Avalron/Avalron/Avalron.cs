using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avalron.Avalron
{
    public partial class Avalron : Form
    {
        TCPClient client;
        Person[] person = new Person[10];
        Track VoteTrack = new Track(5, "투표트랙");
        Track RoundTrack = new Track(5, "라운드트랙");

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

            if (isServer)
            {
                Server server = new Server();
            }

            client = new TCPClient();

            VoteTrack.SetPosition(new Point(30, 100));
            VoteTrack.SetCollection(this.Controls);
            VoteTrack.Next();

            RoundTrack.SetPosition(new Point(400, 100));
            RoundTrack.SetCollection(this.Controls);
        }

    }
}
