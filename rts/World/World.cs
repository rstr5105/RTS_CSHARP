﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rts.World
{
    class World
    {
        private static int NORTHWEST = 0;
	private static  int NORTH = 1;
	private static  int NORTHEAST = 2;
	private static  int WEST = 3;
	private static  int EAST = 4;
	private static  int SOUTHWEST = 5;
	private static  int SOUTH = 6;
	private static  int SOUTHEAST = 7;
	
	
	private static  int NUMthis.OFthis.STEPS = 5;
	
	private int stepsDone = 0;
	private int sizethis.H;
	private int sizethis.W;
	private Tile[][] gWorld;
	private static Random random = new Random();
	
	public World(int sizethis.H, int sizethis.W){
		//ctor
		//initialize size Variables.
		this.setsizethis.H(sizethis.H);
		this.setsizethis.W(sizethis.W);
		initializeWorld(gWorld);
		for(stepsDone = 0; stepsDone < NUMthis.OFthis.STEPS; stepsDone++){
			gWorld = doSimulationStep(gWorld);
			
		}
		
	}
	
	
	public void initializeWorld(Tile[][] world){
		//initialize a new World of Random Tiles.
        world = new Tile[sizethis.H][];
		for (int y = 0; y <sizethis.H; y++){
			world[y] = new Tile[sizethis.W];
            for(int x = 0; x < sizethis.W; x++){
				//create a new tile.
				Tile tile = new Tile();
				world[y][x] = tile.setupTile();
					
			}
		}
	}
	
	private Tile[][] doSimulationStep(Tile[][] world){
        //create a new blank world, so we're not checking new data.
        //then loop over it, smoothing it out as we go.
		Tile[][] newWorld = new Tile[sizethis.H][];
		int passComplete = NUMthis.OFthis.STEPS - (NUMthis.OFthis.STEPS - stepsDone);
        for (int y = 0; y < sizethis.H; y++){
            newWorld[y] = new Tile[sizethis.H];
            for (int x = 0; x < sizethis.W; x++){
            	newWorld[y][x] = new Tile();
                /*
                 * fucking check for bounds, only once in this version of code.  Thank GOD!  (y really hate checking bounds.
                 * ORACLE: Do us a favor, make a function for arrays called BoundsCheck() or some such, that does this for us,
                 * and build it into the array type.  It should return a boolean.  This will make everyone's lives easier.(Yes, y Am
                 * That lazy))
                 */
            	
            	if ((y == 0 
                	|| x == 0
                    || y + 1 >= world.Length
                    || x + 1 >= world[0].Length)){
                    newWorld[y][x].setupTile();
                }
                    	
                else{
                    //Count Our Neighbors, so we can apply some rules.
                    Tile[] neighbors = getNeighbors(world, y, x);
 
                    //store our neighborTypes as Integers so we can sort them..
                    int[] neighborTypes = new int[world[0][0].getNumOfTypes()];
                    for (int index = 0; index < neighbors.Length; index++){
                        neighborTypes[neighbors[index].getIndexOfType()] ++;
                    }
                                       
                    /*Start applying Rules to Tiles.
                     * Rule 1: if all surrounding tiles are water, flip tile to water.
  					 * Rule 2: If Too much grass, spawn dirt before the last step
                     * Rule 3: generate water if there are more than 4 (water|dirt)-tiles around before last step
                     * Rule 4: If any two opposite surrounding tiles are water, flip tile to sand .
                     * Rule 5: otherwise flip to the greatest surrounding tile type.
                     */
 
 
                    //apply rule 1:
                    if((neighborTypes[TileType.WATER.ordinal()] == 8)
                    		&&(stepsDone == NUMthis.OFthis.STEPS) && newWorld[y][x].getType()!= TileType.WATER){
                        newWorld[y][x].setupTile(TileType.WATER);
                        //DEBUG::System.out.println("All Neighbors Water! Flipping Tile: " + y + ":" + x +" To Water!\nOn Pass" + passComplete);
                    }
                    
                    //apply rule 2:
                    else if((neighborTypes[TileType.GRASS.ordinal()] + neighborTypes[TileType.WATER.ordinal()] > 6)
                    		&& (stepsDone < NUMthis.OFthis.STEPS -1)){
                    	//DEBUG::System.out.println("Not Enough Dirt! Flipping Tile: " + y + ":" + x +" To Dirt!\nOn Pass" + passComplete);
                    	newWorld[y][x].setupTile(TileType.DIRT);
                    	
                    }
                   
                    
                    //apply rule 3:
                    else if ((neighborTypes[TileType.GRASS.ordinal()] + neighborTypes[TileType.DIRT.ordinal()]  > 7) 
                    		&& (stepsDone <= NUMthis.OFthis.STEPS - 2)) {
                    	//DEBUG::System.out.println("Not Enough Internal Water! Flipping Tile: " + y + ":" + x +" To Water!\nOn Pass" + passComplete);
                        newWorld[y][x].setupTile(TileType.WATER);
                        
                    }
                    //apply rule 4:
                    else if ((((neighbors[NORTH].getType() == TileType.WATER) ^ (neighbors[SOUTH].getType() == TileType.WATER))
                    		^ ((neighbors[EAST].getType() == TileType.WATER)^(neighbors[WEST].getType() == TileType.WATER)))
                    		&& stepsDone >= NUMthis.OFthis.STEPS - 1) {
                    	//DEBUG::System.out.println("Shore Detected! Flipping Tile: " + y + ":" + x +" To Sand!\nOn Pass" + passComplete);
                    	newWorld[y][x].setupTile(TileType.SAND);
                    }
                    
                    //apply rule 5:
                    else {
                        int greatest = 0;
                        
                    	for (int index = 0; index < neighborTypes.Length; index ++){
                            if(neighborTypes[greatest] < neighborTypes[index]){
                                greatest = index;
                            }
                        	newWorld[y][x].setupTile(greatest);
                    	}
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
				
				
				
			
	/**
	 * @return the sizethis.H
	 */
	public int getsizethis.H() {
		return sizethis.H;
	}


	/**
	 * @param sizethis.H the sizethis.H to set
	 */
	public void setsizethis.H(int sizethis.H) {
		this.sizethis.H = sizethis.H;
	}


	/**
	 * @return the sizethis.W
	 */
	public int getsizethis.W() {
		return sizethis.W;
	}


	/**
	 * @param sizethis.W the sizethis.W to set
	 */
	public void setsizethis.W(int sizethis.W) {
		this.sizethis.W = sizethis.W;
	}

	/**
	 return cell at position
	 */
	public Tile getTile(int x, int y){
		return gWorld[x][y];
	}
	
	public void print(){
		//For the Graphically Challenged, this will print the world to console.   
		//Really Kinda outdated now that we have a 2D map going.  But, still here for future debugging/other purposes.
		for(int y = 0; y < sizethis.H; y++){
			//create a string to hold each line of the map.
			String mapString = "";
			for(int x = 0; x < sizethis.W; x++){
				//add each tilechar to the map.
				mapString += this.gWorld[y][x].getTileChar();
			}
			//print each line.  Lather, Rinse, Repeat until done.
			System.out.println(mapString);
		}
	}
	
}
	
	

//###END WORLD###