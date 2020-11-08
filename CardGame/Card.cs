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

        //These are assigned by the dealer(GameManger)
        public bool vulture = false;
        public bool bomb = false;
        public bool quarantine = false;
        public bool joker = false;

        //Constructor used by the deck class
        public Card(string a, string b)
        {
            this.rank = a;
            this.suit = b;
        }

        //This was for debug purposes, should be deleted
        public void PrintCard()
        {
            Console.WriteLine(rank + " of " + suit);
        }

        //prints the card with both suit and rank
        public string print()
        {
            return rank + " of " + suit;
        }
    }
}
