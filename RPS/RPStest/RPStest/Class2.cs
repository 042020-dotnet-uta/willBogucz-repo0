using System;
using System.Collections.Generic;
using System.Text;

namespace RPStest
{
    public class Game
    {
        public List<Round> Rounds = new List<Round>();
        public Player p1 { get; set; }
        public Player p2 { get; set; }

        private Player _winner;

        public Player Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }



        public Game() { }

        public Game(Player p1, Player p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }
}
