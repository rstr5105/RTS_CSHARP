using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using rts.Units;

namespace rts.Worldgen {



    public class TileType {
        public int id;
        public char Tile;
        public bool Passable;
        public double SpeedMod;
        public string Image;

        public TileType() {

        }
    }
    
    
    public class TileDictionary {
        public enum tileID { WATER = 0, SAND, DIRT, GRASS, PEBBLES, ROCKS, TREES };
    
        
        public TileDictionary() { }
        private int numOfTypes;
        public int NumOfTypes { get { return numOfTypes; } set { numOfTypes = value; } }
        private Dictionary<int, TileType> detailedInfo = new Dictionary<int, TileType>();
        private List<TileType> t = new List<TileType>();
        public List<TileType> T { get { return t; } set { t = value; } }
        public Dictionary<int, TileType> DetailedInfo = new Dictionary<int, TileType>();
        

        public void add(int b, char c, bool bo, float f, string s) {
            TileType TT = new TileType { id = b, Tile = c, Passable = bo, SpeedMod = f, Image = s };
            if(!DetailedInfo.ContainsKey(TT.id)){
                DetailedInfo.Add(TT.id, TT);
            }
            NumOfTypes = DetailedInfo.Count;
                
        }
    }
}

    




    
/**HERE for PORTING PURPOSES ONLY
package game.world;

import game.units.Unit;


public enum TileType {
			//Here we're enumerating out our tile types.  This will make it easier to add tiles in the future.   
			/*
			 * Params:
			 * char tileChar: Debug purposes, just to display the map in console.
			 * boolean passable: Whether or not the tile is passable.
			 * double speedMod: Any units moving through this tile multiply their speed by this.
			 * String sType: The String version of the tile type
			 * Unit[5] unitsInMe: Any units that are in the tile.
			 * String sprite: The image file to load for this tile.
			 */
/*
			WATER('~', false, 0.0, "Water", null, "water.png"),
			SAND('§', true, 0.75, "Sand", null, "sand.png"), 
			DIRT('#', true, 1.0, "Dirt", null, "dirt.png"), 
			GRASS('"', true, 1.0, "Grass", null, "grass.png"),
			PEBBLES('*', true, 1.0, "Pebbles", null, "pebbles.png"),
			ROCK('^', false, 0.0, "Rock", null, "rocks.png"),
			TREE('†', true, 0.5, "Tree", null, "tree.png");
			
			//same member variables as our parent class.
			public String tileImage = "./resources/images/world/";
			char tileChar;
			boolean passable;
			double speedmod;
			String sType;		
			Unit[] unitsInMe;
			String sprite;
			
			private TileType(char tileChar, boolean passable, double speedMod, String sType, Unit[] unitsInMe, String sprite){
				//CTOR
				this.tileChar = tileChar;
				this.passable = passable;
				this.speedmod = speedMod;
				this.sType = sType;
				this.tileImage = tileImage + sprite;
				this.unitsInMe = new Unit[5];
			
			}
			
			
			
};
*/