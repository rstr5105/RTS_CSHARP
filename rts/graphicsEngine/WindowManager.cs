using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;


using SDL2;

namespace rts.graphicsEngine {
	public class WindowManager {
		public bool Quit { get; private set; }

		Size size;
		IntPtr context, window, renderer;

		public WindowManager(int w, int h) : this(new Size(w, h)) {}
		public WindowManager(Size windowSize){
			size = windowSize;

			if (!File.Exists("SDL2-CS.dll.config")){
				Console.WriteLine("SDL2-CS CONFIG NOT FOUND, ABORTING!");


				SDL.SDL_Init ( SDL.SDL_INIT_VIDEO );
				SDL.SDL_GL_SetAttribute ( SDL.SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1 );
				SDL.SDL_GL_SetAttribute ( SDL.SDL_GLattr.SDL_GL_RED_SIZE, 8 );
				SDL.SDL_GL_SetAttribute ( SDL.SDL_GLattr.SDL_GL_GREEN_SIZE, 8 );
				SDL.SDL_GL_SetAttribute ( SDL.SDL_GLattr.SDL_GL_BLUE_SIZE, 8 );
				SDL.SDL_GL_SetAttribute ( SDL.SDL_GLattr.SDL_GL_ALPHA_SIZE, 0 );

				window = SDL.SDL_CreateWindow
					(
						"SDL2 Test Window",
						SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED,
						size.Width, size.Height,
						SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
					);

				renderer = SDL.SDL_CreateRenderer ( window, -1, ( uint )SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED );

				SDL.SDL_ShowCursor ( 1 );
				context = SDL.SDL_GL_CreateContext ( window );
				SDL.SDL_GL_MakeCurrent ( window, context );
			}

			
		}

		public bool initSDL() {
			bool success = true;
			if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO )< 0 ) {
				success = false;
			}
			else{
				SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
				SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_RED_SIZE, 8);
				SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_GREEN_SIZE, 8);
				SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_BLUE_SIZE, 8);
				SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_ALPHA_SIZE, 0);
			}
			return success;
		}
			
			/*/
		    //Returns Current Fullscreen Window
		    public Window getFullScreenWindow(){
			    return device.getFullScreenWindow();
		    }   
	
		    public void restoreScreen(){
			    Window window = device.getFullScreenWindow();
			    if (window != null){
				    window.dispose();
			    }
			    device.setFullScreenWindow(null);
		    }
	
		    public DisplayMode[] getCompatibleModes(){
			    return device.getDisplayModes();
		    }
	
		    public DisplayMode getFirstCompatibleMode(DisplayMode[] modes){
			    DisplayMode[] goodModes = device.getDisplayModes();
			    for (int i = 0; i < modes.length; i++){
				    for(int j = 0; j < goodModes.length; j++){
					    if (displayModesMatch(modes[i], goodModes[j])){
						    return modes[i];
					    }
				    }
			    }
			    return null;
		    }
	
		    public boolean displayModesMatch(DisplayMode mode1, DisplayMode mode2){
			    if (mode1.getWidth() != mode2.getWidth() ||
					mode1.getHeight() != mode2.getHeight()){
						  return false;
			    }

			    if (mode1.getBitDepth() != DisplayMode.BIT_DEPTH_MULTI &&
					mode2.getBitDepth() != DisplayMode.BIT_DEPTH_MULTI &&
					mode1.getBitDepth() != mode2.getBitDepth()){
						  return false;
			    }

			    if (mode1.getRefreshRate() != DisplayMode.REFRESH_RATE_UNKNOWN &&
				    mode2.getRefreshRate() != DisplayMode.REFRESH_RATE_UNKNOWN &&
				    mode1.getRefreshRate() != mode2.getRefreshRate()){
						   return false;
			    }

			    return true;
		    }
	
		    public Graphics2D getGraphics() {
			    // TODO Auto-generated method stub
			    Window window = device.getFullScreenWindow();
			    if (window != null){
				    BufferStrategy strategy = window.getBufferStrategy();
				    return (Graphics2D)strategy.getDrawGraphics();
			    }
			    else{
				    return null;
			    }
		    }
	
		    public void update() {
			    Window window = device.getFullScreenWindow();
			    if (window != null){
				    BufferStrategy strategy = window.getBufferStrategy();
				    if (!strategy.contentsLost()){
					    strategy.show();
				    }
			    }
			    //fix for Linux.  Multi-platform!  YAY!
			    Toolkit.getDefaultToolkit().sync();
		    }
		    public BufferedImage createCompatibleImage(int w, int h, int transparency){
			    Window window = device.getFullScreenWindow();
			    if (window != null){
				    GraphicsConfiguration gc = window.getGraphicsConfiguration();
				    return gc.createCompatibleImage(w, h, transparency);
			    }
			    return null;
		    }
	
		    public int getWidth(){
			    Window window = device.getFullScreenWindow();
			    if (window != null){
				    return window.getWidth();
			    }
			    else{
				    return 0;
			    }
		    }
	
		    public int getHeight(){
			    Window window = device.getFullScreenWindow();
			    if (window != null){
				    return window.getHeight();
			    }
			    else{
				    return 0;
			    }
		    }*/
		}
	}
