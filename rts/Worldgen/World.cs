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


        private static int NUM_OF_STEPS = 0;

        private static Random random = new Random();
        
        //Get our list of Tile Types.
        private static TileDictionary tDict = new TileDictionary();
        

        private Dictionary<int, TileType> td;
        

        private int stepsDone = 0;
        private int Size_H;
        private int Size_W;
        private Tile[][] gWorld;
        public World(int Size_H, int Size_W)
        {

            tDict.add(0, '~', false, 0.0f, "water.png");
            tDict.add(1, '$', true, .75f, "sand.png");
            tDict.add(2, '#', true, .90f, "dirt.png");
            tDict.add(3, '"', true, 1.0f, "grass.png");
            tDict.add(4, '^', false, .85f, "rocks.png");
            tDict.add(5, '%', true, .80f, "pebbles.png");
            tDict.add(6, '!', true, .65f, "trees.png");

            td = tDict.DetailedInfo;
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

                    world[y][x] = new Tile();
                    int rand = random.Next((td.Count - 2) + 2);
                    world[y][x].setupTile(td[rand]);

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
                        newWorld[y][x].setupTile(td[0]);
                    }

                    else{
                        //Count Our Neighbors, so we can apply some rules.
                        Tile[] neighbors = getNeighbors(world, y, x);
                        int[] neighborTypes = new int[td.Count];

                        //store our neighborTypes as Integers so we can sort them..
                       

                        /*
                         * Sort Tiles out by id
                         */ 
                        for (int index = 0; index < neighbors.Length; index++){
                            neighborTypes[neighbors[index].TT.id]++; 
                    }

                        /*Start applying Rules to Tiles.
                         * Rule 1: if all surrounding tiles are water, flip tile to water.
                         * Rule 2: If Too much grass, spawn dirt before the last step
                         * Rule 3: generate water if there are more than 4 (water|dirt)-tiles around before last step
                         * Rule 4: If any two opposite surrounding tiles are water, flip tile to sand .
                         * Rule 5: otherwise flip to the greatest surrounding tile type.
                         */


                        //apply rule 1:
                        if ((neighborTypes[0] ==  8)
                                && (stepsDone == NUM_OF_STEPS) && newWorld[y][x].TT != td[0])
                        {
                            newWorld[y][x].setupTile(td[0]);
                            ////System.Console.WriteLine.WriteLine("All Neighbors Water! Flipping Tile: " + y + ":" + x +" To Water!\nOn Pass" + passComplete);
                        }

                        //apply rule 2:
                        else if ((neighborTypes[1] + neighborTypes[0] > 6)
                                && (stepsDone < NUM_OF_STEPS - 1))
                        {
                            //System.Console.WriteLine.WriteLine("Not Enough Dirt! Flipping Tile: " + y + ":" + x +" To Dirt!\nOn Pass" + passComplete);
                            newWorld[y][x].setupTile(td[2]);
                            
                        }


                        //apply rule 3:
                        else if ((neighborTypes[3] + neighborTypes[2] > 7)
                                && (stepsDone <= NUM_OF_STEPS - 2))
                        {
                            //System.Console.WriteLine.WriteLine("Not Enough Internal Water! Flipping Tile: " + y + ":" + x +" To Water!\nOn Pass" + passComplete);
                            newWorld[y][x].setupTile(td[0]);

                        }
                        //apply rule 4:
                        else if ((((neighbors[NORTH].TT.id == td[0].id) ^ (neighbors[SOUTH].TT.id == td[0].id))
                                ^ ((neighbors[EAST].TT.id == td[0].id) ^ (neighbors[WEST].TT.id == td[0].id)))
                                && stepsDone >= NUM_OF_STEPS - 1){
                            //System.Console.WriteLine.WriteLine("Shore Detected! Flipping Tile: " + y + ":" + x +" To Sand!\nOn Pass" + passComplete);
                            newWorld[y][x].setupTile(td[0]);
                        }

                        //apply rule 5:
                        else
                        {
                            int greatest = 0;
                            
                            for(int index = 0; index < neighborTypes.Length; index++){
                                if (greatest < neighborTypes[index]) {
                                    greatest = neighborTypes[index];
                                }
                                
                            }
                            int mostCommon = Array.IndexOf(neighborTypes, greatest); 
                            newWorld[y][x].setupTile(td[mostCommon]);
                                   
                                
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
				mapString += this.gWorld[y][x].TT.Tile;
			}
			//print each line.  Lather, Rinse, Repeat until done.
			System.Console.WriteLine(mapString);
		    }
	    }

    }
  
    }

	
	

//###END WORLD###
