using System;
using GXPEngine.OpenGL;

namespace GXPEngine.Core {

	public class GLContext {
			
		private int _width;
		private int _height;
		
		private static bool[] keys = new bool[65536];
		private static int[] keyhits = new int[65536];
		private static bool[] buttons = new bool[256];
		private static int[] mousehits = new int[256];
		public static int mouseX = 0;
		public static int mouseY = 0;

		private Game _owner;

		private int _targetFrameRate = 60;
		private long _lastFrameTime = 0;
		private long _lastFPSTime = 0;
		private int _frameCount = 0;
		private int _lastFPS = 0;
		private bool _vsyncEnabled = false;
				
		//------------------------------------------------------------------------------------------------------------------------
		//														RenderWindow()
		//------------------------------------------------------------------------------------------------------------------------
		public GLContext (Game owner) {
			_owner = owner;
			_lastFPS = _targetFrameRate;
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Width
		//------------------------------------------------------------------------------------------------------------------------
		public int width {
			get { return _width; }
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Height
		//------------------------------------------------------------------------------------------------------------------------
		public int height {
			get { return _height; }
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														setupWindow()
		//------------------------------------------------------------------------------------------------------------------------
		public void CreateWindow(int width, int height, bool fullScreen, bool vSync) {
			_width = width;
			_height = height;
			_vsyncEnabled = vSync;
			
			GL.glfwInit();
			
			GL.glfwOpenWindowHint(GL.GLFW_FSAA_SAMPLES, 8);
			GL.glfwOpenWindow(width, height, 8, 8, 8, 8, 24, 0, (fullScreen?GL.GLFW_FULLSCREEN:GL.GLFW_WINDOWED));
			GL.glfwSetWindowTitle("Game");
			GL.glfwSwapInterval(vSync);

			GL.glfwSetKeyCallback(
				(int _key, int _mode) => {
				bool press = (_mode == 1);
				if (press) if (keys[_key] == false) keyhits[_key] = keyhits[_key] + 1;
				keys[_key] = press;
			});

			GL.glfwSetMouseButtonCallback(
				(int _button, int _mode) => {
				bool press = (_mode == 1);
				if (press) if (buttons[_button] == false) mousehits[_button] = mousehits[_button] + 1;
				buttons[_button] = press;
			});
			
			GL.glfwSetWindowSizeCallback((int newWidth, int newHeight) => {
				GL.Viewport(0, 0, newWidth, newHeight);	
				GL.Enable(GL.MULTISAMPLE);	
				GL.Enable (GL.TEXTURE_2D);
				GL.Enable( GL.BLEND );
				GL.BlendFunc( GL.SRC_ALPHA, GL.ONE_MINUS_SRC_ALPHA );
				GL.Hint (GL.PERSPECTIVE_CORRECTION, GL.FASTEST);
				//GL.Enable (GL.POLYGON_SMOOTH);
				
				GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
				
				GL.MatrixMode(GL.PROJECTION);
				GL.LoadIdentity();
				GL.Ortho(0.0f, newWidth, newHeight, 0.0f, 0.0f, 1000.0f);
			});
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														ShowCursor()
		//------------------------------------------------------------------------------------------------------------------------
		public void ShowCursor (bool enable)
		{
			if (enable) {
				GL.glfwEnable(GL.GLFW_MOUSE_CURSOR);
			} else {
				GL.glfwDisable(GL.GLFW_MOUSE_CURSOR);
			}
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														SetScissor()
		//------------------------------------------------------------------------------------------------------------------------
		public void SetScissor(int x, int y, int width, int height) {
			if ((width == _width) && (height == _height)) {
				GL.Disable(GL.SCISSOR_TEST);
			} else {
				GL.Enable(GL.SCISSOR_TEST);
			}
			GL.Scissor(x, y, width, height);

		}
		public void AddScissor(int x, int y, int width, int height) {
			GL.Scissor(x, y, width, height);
			GL.Enable (GL.SCISSOR_TEST);
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Close()
		//------------------------------------------------------------------------------------------------------------------------
		public void Close() {
			GL.glfwCloseWindow();
			GL.glfwTerminate();
			System.Environment.Exit(0);
		}
				
		//------------------------------------------------------------------------------------------------------------------------
		//														Run()
		//------------------------------------------------------------------------------------------------------------------------
		public void Run() {
			GL.glfwSetTime(0.0);
			do {

				if (_vsyncEnabled || (Time.time - _lastFrameTime > (1000 / _targetFrameRate))) {
					_lastFrameTime = Time.time;

					//actual fps count tracker
					_frameCount++;
					if (Time.time - _lastFPSTime > 1000) {
						_lastFPS = (int)(_frameCount / ((Time.time -_lastFPSTime) / 1000.0f));
						_lastFPSTime = Time.time;
						_frameCount = 0;
					}

					UpdateMouse();
					_owner.Step();
					Display();

					Time.newFrame ();
					GL.glfwPollEvents();
				}


			} while (GL.glfwGetWindowParam(GL.GLFW_ACTIVE) == 1);
		}


		//------------------------------------------------------------------------------------------------------------------------
		//														display()
		//------------------------------------------------------------------------------------------------------------------------
		private void Display () {
			GL.Clear(GL.COLOR_BUFFER_BIT);
			
			GL.MatrixMode(GL.MODELVIEW);
			GL.LoadIdentity();
			
			_owner.Render(this);
			
			GL.glfwSwapBuffers();
			//if (GetKey(Key.ESCAPE)) this.Close();
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														SetColor()
		//------------------------------------------------------------------------------------------------------------------------
		public void SetColor (byte r, byte g, byte b, byte a) {
			GL.Color4ub(r, g, b, a);
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														PushMatrix()
		//------------------------------------------------------------------------------------------------------------------------
		public void PushMatrix(float[] matrix) {
			GL.PushMatrix ();
			GL.MultMatrixf (matrix);
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														PopMatrix()
		//------------------------------------------------------------------------------------------------------------------------
		public void PopMatrix() {
			GL.PopMatrix ();
		}
				
		//------------------------------------------------------------------------------------------------------------------------
		//														DrawQuad()
		//------------------------------------------------------------------------------------------------------------------------
		public void DrawQuad(float[] vertices, float[] uv) {
			GL.EnableClientState( GL.TEXTURE_COORD_ARRAY );
			GL.EnableClientState( GL.VERTEX_ARRAY );
			GL.TexCoordPointer( 2, GL.FLOAT, 0, uv);
			GL.VertexPointer( 2, GL.FLOAT, 0, vertices);
			GL.DrawArrays(GL.QUADS, 0, 4);
			GL.DisableClientState(GL.VERTEX_ARRAY);
			GL.DisableClientState(GL.TEXTURE_COORD_ARRAY);			
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														keyDown()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetKey(int key) {
			return keys[key];
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														keyHit()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetKeyDown(int key) {
			bool hit = (keyhits[key] > 0);
			keyhits[key] = 0;
			return hit;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														keyDown()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetMouseButton(int button) {
			return buttons[button];
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														keyHit()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetMouseButtonDown(int button) {
			bool hit = (mousehits[button] > 0);
			mousehits[button] = 0;
			return hit;
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Step()
		//------------------------------------------------------------------------------------------------------------------------
		public static void UpdateMouse() {
			GL.glfwGetMousePos(out mouseX, out mouseY);
		}

		public int currentFps {
			get { return _lastFPS; }
		}

		public int targetFps {
			get { return _targetFrameRate; }
			set {
				if (value < 0) {
					_targetFrameRate = 0;
				} else {
					_targetFrameRate = value;
				}
			}
		}

	}	
	
}

