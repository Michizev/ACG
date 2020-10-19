using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Example
{
	internal class Program
	{
		private static void Main()
		{
			var window = new GameWindow(512, 512); // window with size 512x512 pixel
			var shaderProgram = ShaderTools.CreateShaderProgram(vertexShaderSource, fragmentShaderSource);
			
			void Draw(object sender, FrameEventArgs args)
			{
				//clear screen - what happens without?
				GL.Clear(ClearBufferMask.ColorBufferBit);
				GL.UseProgram(shaderProgram);
				GL.DrawArrays(PrimitiveType.Quads, 0, 4);
				window.SwapBuffers(); // buffer swap needed for double buffering
			}

			window.RenderFrame += Draw; // called once each frame; callback should contain drawing code
			window.Run();
			GL.DeleteProgram(shaderProgram);
		}

		const string vertexShaderSource = @"
				#version 430 core
				out vec3 pos;
				void main() {
					const vec3 vertices[4] = vec3[4](vec3(-0.9, -0.8, 0.5),
                                    vec3( 0.9, -0.9, 0.5),
                                    vec3( 0.9,  0.8, 0.5),
                                    vec3(-0.9,  0.9, 0.5));
					pos = vertices[gl_VertexID];
					gl_Position = vec4(pos, 1.0);
				}";

		const string fragmentShaderSource = @"
			#version 430 core
			in vec3 pos;
			out vec4 color;
			void main() {
				color = vec4(pos + 1.0, 1.0);
			}";
	}
}