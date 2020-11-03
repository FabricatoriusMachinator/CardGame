using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class GameManagr
    {
        private static readonly GameManagr instance = new GameManagr();

        public static Deck playDeck = new Deck();
        public Deck discardDeck = new Deck();

        private static Player player1;
        private static Player player2;
        private static Player player3;
        private static Player player4;

        private static int players = 0;
        public int topOfDiscard = 0;
        public static int topOfDeck = 52;

        public bool won = false;


        private void startGame()
        {     
            playDeck.createDeck();
            playDeck.Shuffle();
            specialCardPrimer();
            setPlayerAmmount();
        }

        public static GameManagr getInstance()
        {
            return instance;
        }

        public Deck getDeck()
        {
            return playDeck;
        }

        private static void setPlayerAmmount()
        {
            Console.WriteLine("Enter number of players between 2 - 4:");

            string playerString = Console.ReadLine();
            players = Convert.ToInt32(playerString);

            if (players < 2 || players > 4)
            {
                Console.WriteLine("Invalid ammount!");
                setPlayerAmmount();
                return;
            }
            else if (players == 3)
            {
                player3 = new Player("Player 3");
            }
            else if(players == 4)
            {
                player3 = new Player("Player 3");
                player4 = new Player("Player 4");
            }

            player1 = new Player("Player 1");
            player2 = new Player("Player 2");

            dealPlayers(players);
            
        }

        private void specialCardPrimer()
        {
            Random random = new Random();
            int randomCard;

            for (int i = 0; i < 4; i++)
            {
                randomCard = random.Next(0, 39);

                if (i == 0)
                {
                    playDeck.deck[randomCard].vulture = true;
                }
                else if (i == 1)
                {
                    playDeck.deck[randomCard].bomb = true;
                }
                else if (i == 2)
                {
                    playDeck.deck[randomCard].quarantine = true;
                }
                else if(i == 3)
                {
                    playDeck.deck[randomCard].joker = true;
                }
            }
        }

        private static void dealPlayers(int playerNumber)
        {
            for (int i = 0; i < playerNumber * 4; i++)
            {
                if (i < 4)
                {
                    player1.hand[i] = playDeck.deck[topOfDeck];
                }
                else if (i < 8 && i > 4)
                {
                    player2.hand[i] = playDeck.deck[topOfDeck];
                }
                else if (i < 12 && i > 8)
                {
                    player3.hand[i] = playDeck.deck[topOfDeck];
                }
                else if(i > 12)
                {
                    player4.hand[i] = playDeck.deck[topOfDeck];
                }
                topOfDeck--;

            }
        }

        private void startPlayers()
        {

        }
    }
}
