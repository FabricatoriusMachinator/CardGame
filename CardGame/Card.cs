using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Card
    {

        private string rank;
        public string suit;

        public bool vulture = false;
        public bool bomb = false;
        public bool quarantine = false;
        public bool joker = false;


        public Card(string a, string b)
        {
            this.rank = a;
            this.suit = b;
        }

        public void PrintCard()
        {
            Console.WriteLine(rank + " of " + suit);
        }

        public string print()
        {
            return rank + " of " + suit;
        }
    }
}
