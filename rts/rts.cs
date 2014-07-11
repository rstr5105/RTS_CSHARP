using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using rts.core;
using rts.Worldgen;
using rts.graphicsEngine;
namespace rts
{
    class rts : GameCore
    {
        static int Main(string[] args)
        {
            World GameWorld = new World(80,80);
            GameWorld.print();
            
            Thread.Sleep(22000);
            return 0;
        }
    }
}
