using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Example
{
	internal class View : IDisposable
	{
		public OrbitingCamera OrbitingCamera { get; }
		private readonly Scene scene;
		private Matrix4 Projection1 = Matrix4.Identity;
		private Matrix4 Projection2 = Matrix4.Identity;
		private Rectangle viewport1;
		private Rectangle viewport2;

		public View()
		{
			scene = new Scene();

			OrbitingCamera = new OrbitingCamera(10f);

			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);
		}

		public void Dispose() => scene.Dispose();

		internal void Draw()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
#if SOLUTION
			GL.Viewport(viewport1);
			scene.Draw(OrbitingCamera.View * Projection1);
			GL.Viewport(viewport2);
			scene.Draw(OrbitingCamera.View * Projection2);
#endif
		}

		internal void Resize(int width, int height)
		{
#if SOLUTION
			var aspect = width / (float)height;
			var splitX = width / 2;
			viewport1 = new Rectangle(0, 0, splitX - 2, height);
			viewport2 = new Rectangle(splitX + 2, 0, splitX - 2, height);

			Projection1 = Matrix4.CreatePerspectiveFieldOfView(1f, aspect, 0.1f, 100.0f);
			Projection2 = Matrix4.CreateOrthographic(aspect * 10f, 10f, 0, 100f);
#endif
		}
	}
}
