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

        public Deck playDeck = new Deck();

        private static Player player1;
        private static Player player2;
        private static Player player3;
        private static Player player4;


        private void startGame()
        {     
            playDeck.createDeck();
            playDeck.Shuffle();
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
            int players = Convert.ToInt32(playerString);

            if (players < 2 || players > 4)
            {
                Console.WriteLine("Invalid ammount!");
                setPlayerAmmount();
                return;
            }
            else if (players == 3)
            {
                player3 = new Player();
            }
            else if(players == 4)
            {
                player3 = new Player();
                player4 = new Player();
            }

            player1 = new Player();
            player2 = new Player();
            
        }

    }
}
