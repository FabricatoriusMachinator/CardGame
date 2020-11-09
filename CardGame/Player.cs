using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame
{
    class Player
    {
        //This class got all the functions that a normal player in real life would have responsibility over such as counter their own cards, draw/discard, and declaring wins
        public Card[] hand = new Card[5];
        GameManagr gameMGMT;
        private static readonly object thisLock = new Object(); 

        string player;
        string popular;
        bool isVulture = false;
        int toSubtract = 1;

        public delegate void onWin();
        public event onWin winEvent;

        public bool hasWon = false;

        public Player(string name)
        {
            this.player = name;
        }

        //Start playing runs as long that no ones declares that they've won. Is called from the GameManager
        public void startPlaying()
        {

            while (!hasWon)

            {
                gameMGMT = GameManagr.Instance;

                checkHand();
                cardTally();

                if (gameMGMT.won == true)
                {
                    break;
                }
                discard();
                drawCard();                
                
            }
            
        }

        //This is the method for drawing
        private void drawCard()
        {
            //this variable is for the vulture card, if vulture is true, the loop will run 5 times

            lock (thisLock)
            {
                if(gameMGMT.topOfDeck < 0)
                {
                    gameMGMT.reShuffle();
                    gameMGMT.topOfDeck = gameMGMT.playDeck.teck.Count();
                    gameMGMT.discardDeck.teck.RemoveRange(0, gameMGMT.discardDeck.teck.Count());
                }


                if (isVulture)
                {
                    toSubtract = 0;
                }

                for (int i = 0; i < hand.Length - toSubtract; i++)
                {


                    if (hand[i] == null)
                    {

                        hand[i] =
                            gameMGMT.playDeck.teck[gameMGMT.topOfDeck]; //is null for some reason
                        Console.WriteLine(printName() + " drew " + hand[i].print());
                        gameMGMT.topOfDeck--;
                        cardHandler(hand[i]);

                    }
                }
            }    
            Random random = new Random();
            int randomWait = random.Next(1000, 3000);
            Thread.Sleep(randomWait);
        }

        //This is just so that I can print the player name in various methods
        public string printName()
        {
            return player;
        }

        //Checks hand for what is most common, this is where the player figures what card to discard;
        public void checkHand()
        {
            //popular = hand[0].suit;
            int counter = 1;
            int tempCounter;
            string temp;

                for(int i = 0; i < (hand.Length - 1); i++)
                {
                    temp = hand[i].suit;
                    tempCounter = 0;

                    for (int j = 1; j < hand.Length; j++)
                    {
                        if (j == 4 && isVulture == false)
                        {
                        continue;
                        }
                        if (hand[j] == null)
                        {
                        drawCard();
                        }
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
                if (hand[i].suit != popular)
                {
                    if (hand[i].joker == true)
                    {
                        continue;
                    }
                    gameMGMT.discardDeck.teck.Add(hand[i]);
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
                
                //Discard entire hand. TODO: make sure to put in discard pile
                Console.WriteLine("BOOM! " + printName() + " drew the bomb");
                for (int i = 0; i < hand.Length - toSubtract; i++)
                {
                    hand[i] = null;
                    
                }
                drawCard();
            }
            else if (drawnCard.quarantine)
            {
                Console.WriteLine("Oh no! It's quarantine for " + printName());
                Thread.Sleep(10000);
            }
            else if (drawnCard.vulture)
            {
                Console.WriteLine("Greedy! " + printName() + " is a Vulture");
                isVulture = true;
            }
        }

        //Here all the cards in the hand are counted and check if if he has won with four of one suit/3 + joker
        public void cardTally()
        {
            int cardTally = 0;
            int counter = 0;

            foreach (Card card in hand)
            {
                if (hand[counter] == null)
                {
                    continue;
                }
                if (hand[counter].suit == popular || hand[counter].joker == true)
                {
                    cardTally++;
                }
                counter++;
                
            }

            if (cardTally == 4)
            {
                hasWon = true;
                gameMGMT.won = true;
                gameMGMT.checkWin();
            }
            
            Thread.Sleep(3000);
        }
    }    
}
