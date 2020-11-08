using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Deck
    {
        //Originally planned to use enums, though felt it was easier to use arrays for this
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
        
        //this creates the card
        public void createDeck()
        {

            int RankCounter = 0;
            int resetCounter = 0;

            //Could've used a nested for loop, but easier to read like this
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

        //Using the Fisher-Yates shuffle algorithm
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
