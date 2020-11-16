using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame
{

    //This class manages the game and is the equivalent of a dealer
    class GameManagr
    {
        private static GameManagr instance = null;

        public Deck playDeck = new Deck();
        public Deck discardDeck = new Deck();

        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;

        //Declaring all the variable ahead of time
        private static Player player1;
        private static Player player2;
        private static Player player3;
        private static Player player4;

        private static List<Player> playerList = new List<Player>();

        private static int players = 0;
        public int topOfDiscard = 0;
        public int topOfDeck = 51;

        public bool won = false;


        //Making sure there is only one instance of the class making it thread safe in order to avoid race conditions
        public static GameManagr Instance
        {
            get
            {
                if (instance == null)
                {
                        if (instance == null)
                        {
                            instance = new GameManagr();
                        }
                    
                }
                return instance;
            }
        }

        //Starts the game and does the dealer job, such as shuffeling the deck, distrubuting card to the number of player, and deciding the special card
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


        //Takes the number of players for the game from the console, can't be lower than 2 or higer than 4
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

            //Decided to use a List here instead of an array due to the varying number of players
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
            int cntr = 0;
            foreach (Player player in playerList)
            {
                cntr++;
            }

            Console.WriteLine(cntr + " players will play this game.");
            

            dealPlayers(players);
            
        }

        //Assign four card special atrributes.
        private void specialCardPrimer()
        {
            Random random = new Random();
            int randomCard;

            for (int i = 0; i < 4; i++)
            {

                //Could do that it is all the card but thought it was up to the dealer to decide what the cards are from the remaining deck
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

        //Takes the number specified and distributes the cards on the number of players
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
            }
        }

        //Check for win, and prints the cards in the winning players hand
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
                        if(card == null)
                        {
                            continue;
                        }
                        if(card.joker == true)
                        {
                            Console.WriteLine(player.hand[cntr].print() + " and is Joker");
                            cntr++;
                            continue;
                        }

                        Console.WriteLine(player.hand[cntr].print());
                        cntr++;
                    }
                    Console.ReadKey();
                }
            }
        }

        public void reShuffle()
        {
            playDeck = discardDeck;
            topOfDeck = playDeck.deck.Count();
            discardDeck.deck.Clear();
            Console.WriteLine("Deck has been reshuffled");
        }



    }
}
