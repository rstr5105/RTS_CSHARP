using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace rts.World
{
    public class Tile
    {
        private TileType type;

        private List<TileType> types = new List<TileType>{TileType.Water, 
                                                          TileType.Sand, 
                                                          TileType.Dirt, 
                                                          TileType.Grass, 
                                                          TileType.Pebbles, 
                                                          TileType.Rock, 
                                                          TileType.Tree};
        

        public TileType getTileType(){
            return this.type;
        }

        public void setType(TileType t){
            this.type = t;
        }

        public int getIndexOfType(TileType t){
            return types.IndexOf(t);
        }
        
    }      
       
        private struct TileType
        {

            public static readonly TileType Water = new TileType('~', false, 0.0, "Water", null, "water.png");
            public static readonly TileType Sand = new TileType('§', true, 0.75, "Sand", null, "sand.png");
            public static readonly TileType Dirt = new TileType('#', true, 1.0, "Dirt", null, "dirt.png");
            public static readonly TileType Grass = new TileType('"', true, 1.0, "Grass", null, "grass.png");
            public static readonly TileType Pebbles = new TileType('*', true, 1.0, "Pebbles", null, "pebbles.png");
            public static readonly TileType Rock = new TileType('^', false, 0.0, "Rock", null, "rocks.png");
            public static readonly TileType Tree = new TileType('†', true, 0.5, "Tree", null, "tree.png");

            private char tile;
            private bool passable;
            private double speedMod;
            private string type;
            private Unit[] unitsInMe;
            private string sprite;

            private TileType(char tile, bool passable, double speedMod, string type, Unit[] unitsInMe, string sprite)
            {
                this.tile = tile;
                this.passable = passable;
                this.speedMod = speedMod;
                this.type = type;
                this.unitsInMe = unitsInMe;
                this.sprite = sprite;


            }
            public char Tile { get { return this.tile; } }
            public bool Passable { get { return this.passable; } }
            public double SpeedMod { get { return this.speedMod; } }
            public string Type { get { return this.type; } }
            public Unit[] UnitsInMe { get { return this.unitsInMe; } }
            public string Sprite { get { return this.sprite; } }
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