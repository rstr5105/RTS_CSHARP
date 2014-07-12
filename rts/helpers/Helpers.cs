using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rts.helpers {
	class Helpers {
		public static bool isBounds( int y, int x, Object[ ][ ] o ) {
			if( y == 0
				|| x == 0
				|| y + 1 >= o.Length
				|| x + 1 >= o[ y ].Length ) {
					return true;
			}
			else {
				return false;
			}
		}
	}
}
