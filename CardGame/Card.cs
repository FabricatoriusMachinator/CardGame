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
        private string suit;

        public Card(string a, string b)
        {
            this.rank = a;
            this.suit = b;
        }

        public void PrintCard()
        {
            Console.WriteLine(rank + " of " + suit);
        }
    }
}
