using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame
{
    class Player
    {

        public Card[] hand = new Card[4];
        GameManagr gameMGMT = GameManagr.Instance;

        string player;
        string popular;
        bool isVulture = false;

        public delegate void onWin();
        public event onWin winEvent;

        public bool hasWon = false;

        public Player(string name)
        {
            this.player = name;
        }

        public void startPlaying()
        {
            while (!gameMGMT.won)

            {
                checkHand();
                discard();
                drawCard();                
                cardTally();
            }
            gameMGMT.checkWin();
        }

        private void drawCard()
        {
            int toSubtract = 1;

            if (isVulture)
            {
                toSubtract = 0;
            }

            for (int i = 0; i < hand.Length - toSubtract; i++)
            {
                

                if (hand[i] == null)
                {
                    
                    hand[i] = 
                        gameMGMT.playDeck.deck[gameMGMT.topOfDeck]; //is null for some reason
                    Console.WriteLine(printName() + " drew " + hand[i].print());
                    cardHandler(hand[i]);
                    gameMGMT.topOfDeck--;
                }
            }
            
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

        //Discard method, called first
        public void discard()
        {
            bool hasDiscarded = false;

            for (int i = 0; i < 4; i++)
            {
                //Check if discard has happened so we don't need to loop through every iteration 
                if (hasDiscarded == true)
                {
                    break;
                }
                //Decission algorithm for what card to discard
                if (hand[i].suit != popular || hand[i].joker != true)
                {
                    gameMGMT.discardDeck.deck[gameMGMT.topOfDiscard] = hand[i];
                    Console.WriteLine(printName() + " discarded " + hand[i].print());
                    hand[i] = null;
                    hasDiscarded = true;
                }

            }
        }

        //This handles all the possible special cards
        public void cardHandler(Card drawnCard)
        {
            if (drawnCard.bomb)
            {
                int count = 0;
                //Discard entire hand. TODO: make sure to put in discard pile
                foreach(Card card in hand)
                {
                    hand[count] = null;
                    count++;
                }
                drawCard();
            }
            else if (drawnCard.quarantine)
            {
                Thread.Sleep(10000);
            }
            else if (drawnCard.vulture)
            {
                isVulture = true;
            }
        }

        public void cardTally()
        {
            int cardTally = 0;
            int counter = 0;

            foreach (Card card in hand)
            {
                if (hand[counter].suit == popular || hand[counter].joker == true)
                {
                    cardTally++;
                }
            }

            if (cardTally == 4)
            {
                hasWon = true;
                gameMGMT.won = true;
            }
        }
    }    
}
