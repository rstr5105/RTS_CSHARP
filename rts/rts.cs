using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using rts.core;
using rts.World;
using rts.graphicsEngine;
namespace rts
{
    class rts : GameCore
    {
        static int Main(string[] args)
        {
            System.Console.WriteLine("Hello World");
            Tile tile = new Tile();
            
            Thread.Sleep(2000);
            return 0;
        }
    }
}
