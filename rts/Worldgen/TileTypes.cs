using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using rts.Units;

namespace rts.Worldgen {



	public struct TileType {
		public int ID;
		public char Tile;
		public bool Passable;
		public double SpeedMod;
		public string Image;

		public TileType(int id, char tile, bool passable, double speedMod, string image) {
			ID = id;
			Tile = tile;
			Passable = passable;
			SpeedMod = speedMod;
			Image = image;

		}
	}

	public struct UnitType {
		public int ID;
		public char Tile;
		public bool Alive;
		public double Speed;
		public String Image;

	}

	public class TileTypes {


		const int WATER = 0;
		const int SAND = 1;
		const int DIRT = 2;
		const int GRASS = 3;
		const int PEBBLES = 4;
		const int ROCKS = 5;
		const int TREES = 6;

		private int numOfTypes;
		public int NumOfTypes { get { return numOfTypes; } set { numOfTypes = value; } }
		private Dictionary<int, TileType> detailedInfo = new Dictionary<int, TileType> ( );
		public Dictionary<int, TileType> DetailedInfo{ get { return detailedInfo; }}
		private List<Object> t = new List<Object> ( );

		public TileTypes() {



			add ( WATER, '~', false, 0.0f, "water.png" );
			add ( SAND, '$', true, .75f, "sand.png" );
			add ( DIRT, '#', true, .90f, "dirt.png" );
			add ( GRASS, '"', true, 1.0f, "grass.png" );
			add ( ROCKS, '^', false, .85f, "rocks.png" );
			add ( PEBBLES, '%', true, .80f, "pebbles.png" );
			add ( TREES, '!', true, .65f, "trees.png" );
		}
		
	public void add( int b, char c, bool bo, float f, string s ) {
			TileType TT = new TileType(b, c,  bo, f, s );
			if ( !DetailedInfo.ContainsKey ( TT.ID ) ) {
				DetailedInfo.Add ( TT.ID, TT );
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