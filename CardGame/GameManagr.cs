using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame
{
    class GameManagr
    {
        private static GameManagr instance = null;

        private static readonly object padLock = new object();

        public Deck playDeck = new Deck();
        public Deck discardDeck = new Deck();

        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;

        private static Player player1;
        private static Player player2;
        private static Player player3;
        private static Player player4;

        private static List<Player> playerList = new List<Player>();

        private static int players = 0;
        public int topOfDiscard = 0;
        public int topOfDeck = 51;

        public bool won = false;


        public static GameManagr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padLock)
                    {
                        if (instance == null)
                        {
                            instance = new GameManagr();
                        }
                    }
                }
                return instance;
            }
        }

        public void startGame()
        {     
            playDeck.createDeck();
            playDeck.Shuffle();
            specialCardPrimer();
            setPlayerAmmount();
            Console.WriteLine("Press any key when ready to start:");
            Console.ReadKey();
            Console.Clear();
            startPlayers();

        }


        private void setPlayerAmmount()
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

            player1 = new Player("Player 1");
            player2 = new Player("Player 2");
            thread1 = new Thread(player1.startPlaying);           
            thread2 = new Thread(player2.startPlaying);
            playerList.Add(player1);
            playerList.Add(player2);

            if (players == 3)
            {
                player3 = new Player("Player 3");
                thread3 = new Thread(player3.startPlaying);
                playerList.Add(player3);
            }
            else if(players == 4)
            {
                player3 = new Player("Player 3");
                player4 = new Player("Player 4");
                thread3 = new Thread(player3.startPlaying);
                thread4 = new Thread(player4.startPlaying);
                playerList.Add(player3);
                playerList.Add(player4);
            }

            

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

        private void dealPlayers(int playerNumber)
        {

            int cntr1 = 0;
            int cntr2 = 0;
            int cntr3 = 0;
            int cntr4 = 0;

            for (int i = 0; i < playerNumber * 4; i++)
            {
                if (i < 4)
                {
                    player1.hand[cntr1] = playDeck.deck[topOfDeck - 1];
                    cntr1++;
                }
                else if (i < 8 && i > 3)
                {
                    player2.hand[cntr2] = playDeck.deck[topOfDeck];
                    cntr2++;
                }
                else if (i < 12 && i > 7)
                {
                    player3.hand[cntr3] = playDeck.deck[topOfDeck];
                    cntr3++;
                }
                else if(i > 11)
                {
                    player4.hand[cntr4] = playDeck.deck[topOfDeck];
                    cntr4++;
                }
                topOfDeck--;

            }
        }

        private void startPlayers()
        {
            int cntr = 0;
            foreach (Player playerInGame in playerList)
            {
                if (cntr == 0)
                {
                    thread1.Start();
                    cntr++;
                }
                else if (cntr == 1)
                {
                    thread2.Start();
                    cntr++;
                }
                
                else if (cntr == 2)
                {
                    thread3.Start();
                    cntr++;
                }
                else if(cntr == 3)
                {
                    thread4.Start();
                }

                Thread.Sleep(200);
            }
        }

        public void checkWin()
        {
            int cntr = 0;
            foreach(Player player in playerList)
            {
                if (player.hasWon == true)
                {
                    Console.WriteLine(player.printName() + " won with:");

                    foreach (Card card in player.hand)
                    {
                        Console.WriteLine(player.hand[cntr].print());
                        cntr++;
                    }
                    Console.ReadKey();
                }
            }
        }
    }
}
