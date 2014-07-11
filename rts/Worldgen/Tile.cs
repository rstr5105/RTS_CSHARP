using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using rts.Worldgen;

namespace rts {
    public class Tile {

        //member variables

        private TileType tt;
        public TileType TT { get { return tt;}set{tt = value;} }
        private static Random random = new Random();
        public Tile() {
            //Default CTOR
            
        }
        
        public void setupTile(TileType tt) {
            TT = tt;
        }
        
        
    }
}
	
	
		
	
	/*****************************
	 * Getters and setters.  blah.  
	 *****************************
	public TileType getTypeByIndex(int i){
		//The Cleverest Function that could.   When the Tile() constructor calls this from createTile(TileType.getTypeByIndex)
		//It simply returns the values we need.  Genius!  To Think, ten minutes ago I was about to give up on the enum type.  
			return type;
		}
	
	public int getNumOfTypes(){
		return numOfTypes;
	}
	
	public bool isPassable() {
		/*
		 * @Return passable
		 *
		return type.Passable;
	}

	
	//public int getIndexOfType(){
		//return this.getType().ordinal();
	}
	
	/**
	 * @return the type
	 *
	//public TileType getType() {
		//return type;
	//}

	/**
	 * @param type the type to set
	 *
	public void setType(TileType type) {
		this.type = type;
	}

	public double getSpeedMod() {
		/**
		 * @return the speedMod.  This will tell units what percentage of their maximum speed they can move across this soil.
		 *
		return type.SpeedMod;
	}

	public String getSType(){
		/**
		 * @return the SType
		 *
		return type.Type;
	}

	public char getTileChar() {
		/**
		 * @return the tileChar
		 *
		return type.Tile;
	}
	
	
	public Tile setupTile(){
		//This function was made much neater by the use of an enum.   
		//check out the cleverest little function that could. 
		//This function basically just izes creating a tile, by grabbing the appropriate 
		//information from TileType and returning a new tile.  
		//thanks to everyone in ##Java on Freenode for their help.
		TileType typeSelector = getTypeByIndex(random.(typeSelector.getNumOfTypes() - 2) + 2);
		//here we'll create a tile, and then initialize all of it's variables based on our enum.  *PRETTY CLEVER!*
		this.setType((typeSelector));
		return this;
	}
	
	/**
	 * @deprecated 
	 * @overloaded function
	 * @param typeSelector
	 * @return
	 *
    
    [Obsolete ("This Method is now Obsolete")]
	public Tile setupTile(int typeSelector){
		//This function over loads setupTile so that it can be used to modify a tile later on.  
		TileType typeSelected = getTypeByIndex(typeSelector);
		//here we'll create a tile, and then initialize all of it's variables based on our enum.  *PRETTY CLEVER!*
		this.setType((typeSelected));
		return this;
	}
	
	public Tile setupTile(TileType type){
		//This function over loads setupTile so that it can be used to modify a tile later on.  
		//here we'll create a tile, and then initialize all of it's variables based on our enum.  *PRETTY CLEVER!*
		this.setType((type));
		return this;
	}
	public void printTileInfo(){
		//Later to be deprecated with Tile::printTileToolTip
		System.Console.WriteLine(this.getType() + "\nPassable: " + this.isPassable() + "\nSpeedMod: " + this.getSpeedMod());
	}
}
//###End Tile###
}
    */