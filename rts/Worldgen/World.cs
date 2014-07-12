using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using rts.helpers;

namespace rts.Worldgen {
	class World {


		//enumerate our Tile Ids
		public static enum tileType {
			WATER,
			SAND,
			DIRT,
			GRASS,
			PEBBLES,
			ROCKS,
			TREES
		};
		public enum directions {
			NORTHWEST,
			NORTH,
			NORTHEAST,
			WEST,
			EAST,
			SOUTHWEST,
			SOUTH,
			SOUTHEAST,

		}


		//How many times are we going to loop over the world
		const int NUM_OF_STEPS = 3;



		private Random random = new Random ( );

		//Create our list of Acceptable Tile Types.
		private TileTypes tTypes = new TileTypes ( );


		private Dictionary<int, TileType> TileDictionary;
		private int stepsDone = 0;
		private int Size_H;
		private int Size_W;
		private Tile[ ][ ] gWorld;
		public World( int Size_H, int Size_W ) {

			TileDictionary = tTypes.DetailedInfo;
			//ctor
			//initialize size Variables.
			this.Size_H = Size_H;
			this.Size_W = Size_W;

			gWorld = initializeWorld ( );
			for ( stepsDone = 0; stepsDone < NUM_OF_STEPS; stepsDone++ ) {
				gWorld = doSimulationStep ( gWorld );
			}
		}


		public Tile[ ][ ] initializeWorld() {
			//initialize a new World of Random Tiles.
			Tile[ ][ ] world = new Tile[ Size_H ][ ];
			for ( int y = 0; y < Size_H; y++ ) {
				world[ y ] = new Tile[ Size_W ];
				for ( int x = 0; x < Size_W; x++ ) {
					//create a new tile, and randomly initialize it.
					world[ y ][ x ] = new Tile ( );
					int rand = random.Next ( ( TileDictionary.Count - 2 ) + 2 );
					world[ y ][ x ].setupTile ( TileDictionary[ rand ] );
				}
			}
			return world;
		}

		private Tile[ ][ ] doSimulationStep( Tile[ ][ ] world ) {
			//create a new blank world, so we're not checking new data.
			//then loop over it, smoothing it out as we go.
			Tile[ ][ ] newWorld = copyWorld ( world );
			int passComplete = NUM_OF_STEPS - ( NUM_OF_STEPS - stepsDone );
			for ( int y = 0; y < Size_H; y++ ) {
				for ( int x = 0; x < Size_W; x++ ) {
					if ( !Helpers.isBounds ( y, x, world ) ) {
						newWorld[ y ][ x ].setupTile ( TileDictionary[ ( int )tileType.WATER ] );
					}
					else {
						//Count Our Neighbors, so we can apply some rules.
						Tile[ ] neighbors = getNeighbors ( world, y, x );
						int[ ] neighborTypes = new int[ TileDictionary.Count ];
						for ( int i = 0; i < neighbors.Length; i++ ) {
							neighborTypes[ neighbors[ i ].ID ]++;
						}
						//apply Rules
						newWorld[ y ][ x ] = applyRules ( world[ y ][ x ], neighbors, neighborTypes, y, x );
					}
				}
			}
			System.Console.WriteLine ( "{0} Passes Complete", passComplete );
			return newWorld;
		}

		private Tile[ ][ ] copyWorld( Tile[ ][ ] world ) {
			/*
			 * Copies a world into a new one.
			 */
			Tile[ ][ ] newWorld = new Tile[ Size_H ][ ];
			for ( int y = 0; y < Size_H; y++ ) {
				newWorld[ y ] = new Tile[ Size_W ];
				for ( int x = 0; x < Size_W; x++ ) {
					newWorld[ y ][ x ] = world[ y ][ x ];
				}
			}
			return newWorld;
		}


		private bool checkForBounds( Tile[ ][ ] world, int y, int x ) {
			if ( y == 0
			|| x == 0
			|| y + 1 >= world.Length
			|| x + 1 >= world[ y ].Length ) {
				return true;
			}
			else {
				return false;
			}
		}
		private Tile[ ] getNeighbors( Tile[ ][ ] world, int y, int x ) {
			//Create a Tile Arrax to store all 8 of our neighbors in.  This makes things so much easier than what I was doing before.
			//Check for bounds:
			if ( !Helpers.isBounds ( y, x, world ) ) {
				Tile[ ] neighbors = {world[y - 1][x - 1], world[y - 1][x],	world[y - 1][x + 1], 
								world[y][x -1],		 			world[y][x+1], 
								world[y + 1][ x-1],  world[y + 1][x],	world[y + 1][x + 1]};
				return neighbors;
			}
			else {
				//should never, ever, ever happen
				throw new Exception ( "Uh-Oh!  Something went wrong while bounds checking!" );
			}

		}

		private Tile applyRules( Tile tile, Tile[ ] neighbors, int[ ] neighborTypes, int y, int x ) {
			Tile newTile = new Tile ( );

			if ( stepsDone == NUM_OF_STEPS - 1 ) {
				newTile = applyRule1 ( tile, neighbors, neighborTypes );
				newTile = applyRule4 ( tile, neighbors, neighborTypes );


			}

			if ( stepsDone <= NUM_OF_STEPS ) {

			}
			if ( stepsDone <= NUM_OF_STEPS - 1 ) {
				newTile = applyRule2 ( tile, neighbors, neighborTypes );
				newTile = applyRule3 ( tile, neighbors, neighborTypes );


			}
			if ( stepsDone <= NUM_OF_STEPS - ( NUM_OF_STEPS / 2 ) ) {
				newTile = applyRule5 ( tile, neighborTypes );


			}

			System.Console.WriteLine ( "Rules Applied To Tile {0} {1}  Passes Complete: {2}", y, x, stepsDone );

			print ( );
			Thread.Sleep ( 50 );
			System.Console.WriteLine ( "" );
			return newTile;
		}

		private Tile applyRule1( Tile tile, Tile[ ] neighbors, int[ ] neighborTypes ) {
			Tile newTile = tile;
			/*
			 * Apply rule 1: On the very last step, if Tile t is not already water 
			 * and all of its neighbors are, change t to water. 
			 */

			if ( ( neighborTypes[ (int)tileType.WATER ] == 8 ) && ( newTile.ID != (int)tileType.WATER ) ) {
				newTile.setupTile ( TileDictionary[ (int)tileType.WATER ] );
			}
			return newTile;
		}

		private Tile applyRule2( Tile tile, Tile[ ] neighbors, int[ ] neighborTypes ) {
			Tile newTile = tile;
			/*
			 * Apply rule 2: prior to the last step, if there is too much grass or water 
			 * on the interior of the map change tile t to dirt.
			 */
			if ( ( neighborTypes[ (int)tileType.GRASS ] + neighborTypes[ (int)tileType.WATER ] > 6 )
				&& ( stepsDone < NUM_OF_STEPS - 1 )  {
				newTile.setupTile ( TileDictionary[ (int)tileType.DIRT ] );
			}
			return newTile;
		}

		private Tile applyRule3( Tile tile, Tile[ ] neighbors, int[ ] neighborTypes ) {
			Tile newTile = tile;

			/* 
			 * Apply Rule 3: Prior to the last step, if there is too much sand or dirt 
			 * on the interior of the map, change tile t to water.
			 */
			if ( ( neighborTypes[ (int)tileType.GRASS ] + neighborTypes[ (int)tileType.DIRT ] > 7 ) ) {
				newTile.setupTile ( TileDictionary[ (int)tileType.WATER ] );
			}
			return newTile;
		}

		private Tile applyRule4( Tile tile, Tile[ ] neighbors, int[ ] neighborTypes ) {
			Tile newTile = tile;

			/* 
			 * Apply Rule 4: all the way up to the last step, if any direct neighbor is water, and the opposite
			 * neighbor is not, change tile t to sand.
			 */
			bool shore = ( ( ( neighbors[ (int)directions.NORTH ].ID == (int)tileType.WATER ) ^ neighbors[ (int)directions.SOUTH ].ID == (int)tileType.WATER ) ^ ( neighbors[ (int)directions.WEST ].ID == (int)tileType.WATER ) ^ ( neighbors[ (int)directions.EAST ].ID == (int)tileType.WATER ) );
			System.Console.WriteLine ( shore );
			if ( shore ) {
				newTile.setupTile ( TileDictionary[ SAND ] );
			}
			return newTile;
		}

		private Tile applyRule5( Tile tile, int[ ] neighborTypes ) {

			Tile newTile = tile;

			/*
			 * Apply Rule 5: Prior to halfway through the loop, find the greatest number of neighboring tiles,
			 * and sometimes flip Tile t to that TileType.
			 */
			int greatest = 0;
			for ( int index = 0; index < neighborTypes.Length; index++ ) {
				if ( greatest < neighborTypes[ index ] ) {
					greatest = neighborTypes[ index ];
				}
			}
			int mostCommon = Array.IndexOf ( neighborTypes, greatest );
			float chanceToFlip = 0.25f;
			float rn = ( float )random.NextDouble ( );
			if ( rn < chanceToFlip ) {
				newTile.setupTile ( TileDictionary[ mostCommon ] );
			}
			return newTile;
		}



		public void print() {
			//For the Graphically Challenged, this will print the world to console.   
			//Really Kinda outdated now that we have a 2D map going.  But, still here for future debugging/other purposes.
			for ( int y = 0; y < Size_H; y++ ) {
				//create a string to hold each line of the map.
				String mapString = "";
				for ( int x = 0; x < Size_W; x++ ) {
					//add each tilechar to the map.
					mapString += gWorld[ y ][ x ].TT.Tile;
				}
				//print each line.  Lather, Rinse, Repeat until done.
				System.Console.WriteLine ( mapString );
			}
		}

	}

}




//###END WORLD###
