using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using  rts.graphicsEngine;

namespace rts.core
{
    abstract class GameCore
    {
        /*private bool isRunning;
	    protected WindowManager wm;
	
	    public void stop(){
		    isRunning = false;
	    }
	
	
	    public void run(){
		    try{
			    init();
			    gameLoop();
		    }
		    finally{
			    wm.restoreScreen();
			    exit();
		    }
	    }
	
	    public void exit(){
	    }
	
	    public void init(){
		    wm = new WindowManager();
		    DisplayMode displayMode = wm.getFirstCompatibleMode(POSSIBLE_MODES);
		    wm.setFullScreen(displayMode);
		    Window window = wm.getFullScreenWindow();
		    window.setFont(new Font("Dialog", Font.PLAIN, FONT_SIZE));
		    window.setBackground(Color.BLACK);
		    window.setForeground(Color.WHITE);
		    isRunning = true;
	    }
	
	    public Image loadImage(String filename){
		    return new ImageIcon(filename).getImage();
	    }
	
	    public void gameLoop(){
		    long startTime = System.currentTimeMillis();
		    long currTime = startTime;
		
		    while(isRunning){
			    long elapsedTime = System.currentTimeMillis() - currTime;
			    currTime += elapsedTime;
			
			    update(elapsedTime);
			
			    Graphics2D g = wm.getGraphics();
			    draw(g);
			    g.dispose();
			    wm.update();
		    }
	    }
	
	    public void update(long elapsedTime){
		    //SKIP!
	    }
	
        /**
	    * draws to the screen.  Sub-classes MUST OVERRIDE!
	    * @param g
	    
	    public abstract void draw(Graphics2D g);
    */
    }
}
   
