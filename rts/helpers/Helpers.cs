using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rts.helpers {
	class Helpers {
		public static bool isBounds( int y, int x, Object[ ][ ] array ) {
			
			if(	 y == 0
				|| x == 0
				|| y + 1 >= array.Length
				|| x + 1 >= array[ y ].Length ) {
					return true;
			}
			return false;
		}
	}
}
