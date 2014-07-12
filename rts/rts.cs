using System;
using System.Diagnostics;
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
	    static WindowManager wm;
        static int Main(string[] args)
        {
		   wm = new WindowManager ( 800,600 );

		   while ( !wm.Quit ) {

		   }
            const int SIZE_H = 30;
            const int SIZE_W = 30;
            Stopwatch t = new Stopwatch();
            t.Start();
		  World GameWorld = new World(SIZE_H, SIZE_W);
            t.Stop();
            long l = t.ElapsedMilliseconds;
            t.Reset();
            t.Start();
            GameWorld.print();
            t.Stop();
            long m = t.ElapsedMilliseconds;
            System.Console.WriteLine("Generated a {0} x {1} World in: {2} seconds", SIZE_H, SIZE_W, (double)l / 1000);
            System.Console.WriteLine("And Printed it in {0} seconds", (double)m / 1000);

            Thread.Sleep(22000);
            return 0;
        }
    }
}
