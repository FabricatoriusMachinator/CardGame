using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    //Facade
    class GameMain
    {
        private static GameManagr gameMGMT = GameManagr.Instance;
        
        static void Main()
        {

            gameMGMT.startGame();
        }
    }
}

