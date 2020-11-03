using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame
{
    class Player
    {

        public Card[] hand = new Card[3];
        GameManagr gameMGMT = GameManagr.getInstance();

        int cardCount = 0;
        int cardCap = 4;
        int index = 0; //TODO: Create selection algorithm

        string player;
        int cardScore;
        string popular; 

        public Player(string name)
        {
            this.player = name;
        }

        private void drawCard()
        {
            
            Random random = new Random();
            int randomWait = random.Next(1000, 3000);
            Thread.Sleep(randomWait);
        }

        public string printName()
        {
            return player;
        }

        public void checkHand()
        {
            popular = hand[0].suit;
            int counter = 1;
            int tempCounter;
            string temp;

                for(int i = 0; i < (hand.Length - 1); i++)
                {
                    temp = hand[i].suit;
                    tempCounter = 0;

                    for (int j = 1; j < hand.Length; j++)
                    {
                        if (temp == hand[j].suit)
                        {
                            tempCounter++;
                        }
                    }

                    if (tempCounter > counter)
                    {
                        popular = temp;
                        counter = tempCounter;
                    }
                }
        }

        
        public void discard()
        {
            bool hasDiscarded = false;

            for (int i = 0; i < 4; i++)
            {
                if (hand[i].suit != popular || hand[i].joker != true)
                {
                    gameMGMT.discardDeck.deck[gameMGMT.topOfDiscard] = hand[i];
                    hand[i] = null;
                    hasDiscarded = true;
                }
                else if(hasDiscarded == true)
                {
                    break;
                }
            }
        }
    }    
}
