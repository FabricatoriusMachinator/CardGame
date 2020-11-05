using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class GameMain
    {
        private static GameManagr gameMGMT = GameManagr.Instance;

        static void Main()
        {

            gameMGMT.startGame();
            
            
            
            /*Deck playDeck = new Deck();
            playDeck.createDeck();
            
            for(int i = 0; i < playDeck.deck.Length; i++)
            {
                playDeck.deck[i].PrintCard();
            }

            Console.ReadKey();
            Console.Clear();
            playDeck.Shuffle();

            for (int i = 0; i < playDeck.deck.Length; i++)
            {
                if (playDeck.deck[i] == null)
                {
                    Console.WriteLine("Null");
                }

                playDeck.deck[i].PrintCard();
            }

            Console.ReadKey();*/
        }
    }
}

