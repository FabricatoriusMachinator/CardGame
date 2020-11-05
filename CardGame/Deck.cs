using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Deck
    {
        string[] Suit =
        {
            "Diamonds",
            "Hearts",
            "Clubs",
            "Spades"
        };

        string[] Rank = 
        {
            "Ace",
            "Two",
            "Three",
            "Four",
            "Five",
            "Six",
            "Seven",
            "Eight",
            "Nine",
            "Ten",
            "Jack",
            "Queen",
            "King"
        };

        public Card[] deck = new Card[52];
        

        public void createDeck()
        {

            int RankCounter = 0;
            int resetCounter = 0;

            for (int i=0; i < deck.Length; i++)
            {

                if(RankCounter >= 13)
                {
                    RankCounter = 0;
                    resetCounter++;
                }

                deck[i] = new Card(Rank[RankCounter], Suit[resetCounter]);
                RankCounter++;
            }
        }

        public void Shuffle()
        {
            int deckLength = deck.Length;
            Card temp;
            Random random = new Random();
            
            
            while(0 != deckLength)
            {
                int intRandom = random.Next(0, 51);
                deckLength--;
                temp = deck[deckLength];
                deck[deckLength] = deck[intRandom];
                deck[intRandom] = temp;
            }
        }



    }
}
