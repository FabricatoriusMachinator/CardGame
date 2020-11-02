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
        
        private void startGame()
        {
            playDeck.createDeck();
            playDeck.Shuffle();
        }

        public static GameManagr getInstance()
        {
            return instance;
        }

        public Deck getDeck()
        {
            return playDeck;
        }

    }
}
