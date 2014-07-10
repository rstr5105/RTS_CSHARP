using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rts.Worldgen {
    class World {
        private static int NORTHWEST = 0;
        private static int NORTH = 1;
        private static int NORTHEAST = 2;
        private static int WEST = 3;
        private static int EAST = 4;
        private static int SOUTHWEST = 5;
        private static int SOUTH = 6;
        private static int SOUTHEAST = 7;


        private static int NUM_OF_STEPS = 5;

        private static Random random = new Random();
        
        //Get our list of Tile Types.
        private static TileDictionary tDict = new TileDictionary();
        private static Dictionary<string, TileType> tileTypes = tDict.DetailedInfo;
        //so we can use a random key.
        private static string[] keys = tileTypes.Keys.ToArray();

        

        private int stepsDone = 0;
        private int Size_H;
        private int Size_W;
        private Tile[][] gWorld;
        
        public World(int Size_H, int Size_W)
        {
            //ctor
            //initialize size Variables.
            this.Size_H = Size_H;
            this.Size_W = Size_W;
            
            gWorld = initializeWorld();
            for (stepsDone = 0; stepsDone < NUM_OF_STEPS; stepsDone++)
            {
                gWorld = doSimulationStep(gWorld);

            }

        }


        public Tile[][] initializeWorld()
        {
            //initialize a new World of Random Tiles.
            Tile[][] world = new Tile[Size_H][];
            for (int y = 0; y < Size_H; y++)
            {
                world[y] = new Tile[Size_W];
                for (int x = 0; x < Size_W; x++)
                {
                    //create a new tile, and initialize it to Water.
                   
                    world[y][x] = new Tile(tileTypes["WATER"]);

                }
            }
            return world;
        }

        private Tile[][] doSimulationStep(Tile[][] world)
        {
            //create a new blank world, so we're not checking new data.
            //then loop over it, smoothing it out as we go.
            Tile[][] newWorld = new Tile[Size_H][];
            int passComplete = NUM_OF_STEPS - (NUM_OF_STEPS - stepsDone);
            for (int y = 0; y < Size_H; y++){
                newWorld[y] = new Tile[Size_W];
                for (int x = 0; x < Size_W; x++){
                    newWorld[y][x] = world[y][x];
                    /*
                     * fucking check for bounds, only once in this version of code.  Thank GOD!  (y really hate checking bounds.
                     * ORACLE: Do us a favor, make a function for arrays called BoundsCheck() or some such, that does this for us,
                     * and build it into the array type.  It should return a boolean.  This will make everyone's lives easier.(Yes, y Am
                     * That lazy))
                     */

                    if ((y == 0
                        || x == 0
                        || y + 1 >= world.Length
                        || x + 1 >= world[y].Length)){
                        newWorld[y][x].setupTile(tileTypes["WATER"]);
                    }

                    else{
                        //Count Our Neighbors, so we can apply some rules.
                        Tile[] neighbors = getNeighbors(world, y, x);

                        //store our neighborTypes as Integers so we can sort them..
                        Dictionary<TileType, int>neighborTypes = new Dictionary<TileType, int>();
                        foreach (KeyValuePair<string, TileType> kvp in tileTypes) {
                            neighborTypes.Add(kvp.Value, 0);
                        }

                        for (int index = 0; index < neighbors.Length; index++){
                            System.Console.WriteLine(neighbors[index].Type.Name);
                            neighborTypes[neighbors[index].Type] += 1;
                            
                    }

                        /*Start applying Rules to Tiles.
                         * Rule 1: if all surrounding tiles are water, flip tile to water.
                         * Rule 2: If Too much grass, spawn dirt before the last step
                         * Rule 3: generate water if there are more than 4 (water|dirt)-tiles around before last step
                         * Rule 4: If any two opposite surrounding tiles are water, flip tile to sand .
                         * Rule 5: otherwise flip to the greatest surrounding tile type.
                         */


                        //apply rule 1:
                        if ((neighborTypes[tileTypes["WATER"]] ==  8)
                                && (stepsDone == NUM_OF_STEPS) && newWorld[y][x].Type != tileTypes["WATER"])
                        {
                            newWorld[y][x].setupTile(tileTypes["WATER"]);
                            //DEBUG::System.out.println("All Neighbors Water! Flipping Tile: " + y + ":" + x +" To Water!\nOn Pass" + passComplete);
                        }

                        //apply rule 2:
                        else if ((neighborTypes[tileTypes["GRASS"]] + neighborTypes[tileTypes["WATER"]] > 6)
                                && (stepsDone < NUM_OF_STEPS - 1))
                        {
                            //DEBUG::System.out.println("Not Enough Dirt! Flipping Tile: " + y + ":" + x +" To Dirt!\nOn Pass" + passComplete);
                            newWorld[y][x].setupTile(tileTypes["DIRT"]);
                            
                        }


                        //apply rule 3:
                        else if ((neighborTypes[tileTypes["GRASS"]] + neighborTypes[tileTypes["DIRT"]] > 7)
                                && (stepsDone <= NUM_OF_STEPS - 2))
                        {
                            //DEBUG::System.out.println("Not Enough Internal Water! Flipping Tile: " + y + ":" + x +" To Water!\nOn Pass" + passComplete);
                            newWorld[y][x].setupTile(tileTypes["WATER"]);

                        }
                        //apply rule 4:
                        else if ((((neighbors[NORTH].Equals(tileTypes["WATER"]) ^ (neighbors[SOUTH].Equals(tileTypes["WATER"])))
                                ^ ((neighbors[EAST].Equals(tileTypes["WATER"]) ^ (neighbors[WEST].Equals(tileTypes["WATER"]))))
                                && stepsDone >= NUM_OF_STEPS - 1))){
                            //DEBUG::System.out.println("Shore Detected! Flipping Tile: " + y + ":" + x +" To Sand!\nOn Pass" + passComplete);
                            newWorld[y][x].setupTile(tileTypes["SAND"]);
                        }

                        //apply rule 5:
                        else
                        {

                            

                            int greatest = 0;
                            TileType t = tileTypes["WATER"];
                            foreach (KeyValuePair<TileType, int> kvp in neighborTypes) {
                                if (greatest < kvp.Value) {
                                    greatest = kvp.Value;
                                    t = kvp.Key;
                                }
                                
                            }
                            newWorld[y][x].setupTile(t);
                            }
                        }
                    }
                }
            return newWorld;
        }
            
        
        
        private Tile[] getNeighbors(Tile[][] world, int y, int x){
            //Create a Tile Arrax to store all 8 of our neighbors in.  This makes things so much easier than what I was doing before.
            Tile[] neighbors = {world[y - 1][x - 1], world[y - 1][x], world[y - 1][x + 1], 
							    world[y][x -1],		 						world[y][x+1], 
							    world[y + 1][x-1],  world[y + 1][x],  world[y + 1][x + 1]};
            return neighbors;
        }
        
        public void print(){
		//For the Graphically Challenged, this will print the world to console.   
		//Really Kinda outdated now that we have a 2D map going.  But, still here for future debugging/other purposes.
		for(int y = 0; y < Size_H; y++){
			//create a string to hold each line of the map.
			String mapString = "";
			for(int x = 0; x < Size_W; x++){
				//add each tilechar to the map.
				mapString += this.gWorld[y][x].Type.Tile;
			}
			//print each line.  Lather, Rinse, Repeat until done.
			System.Console.WriteLine(mapString);
		    }
	    }

    }
  
    }

	
	

//###END WORLD###
