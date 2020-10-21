using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;

namespace Example
{
	internal class View : IDisposable
	{
		private readonly MyPointSpriteShaderProgram shaderPointSprite;
		private readonly Stopwatch time;
		private readonly VertexArrayVector4 vertexArray;

		public View()
		{
			shaderPointSprite = new MyPointSpriteShaderProgram();

			//generate starting positions and velocity array on CPU
			var rnd = new Random(12);
			float Rnd01() => (float)rnd.NextDouble();
			float RndCoord() => (Rnd01() - 0.5f) * 2.0f;
			float RndSpeed() => (Rnd01() - 0.5f) * 0.1f;
			var positionVelocity = new Vector4[500];
			for (int i = 0; i < positionVelocity.Length; ++i)
			{
				positionVelocity[i] = new Vector4(RndCoord(), RndCoord(), RndSpeed(), RndSpeed());
			}
			// copy position and velocity data to GPU
			vertexArray = new VertexArrayVector4(PrimitiveType.Points, positionVelocity, shaderPointSprite.LocationPositionVelocity);

			time = Stopwatch.StartNew();

			// additive blend mode
			GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);
			GL.Enable(EnableCap.Blend);
		}

		public void Dispose()
		{
			shaderPointSprite.Dispose();
		}

		internal void Draw()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			////ATTENTION: always give the time as a float if the uniform in the shader is a float
			shaderPointSprite.Activate((float)time.Elapsed.TotalSeconds, 0.07f);
			vertexArray.Draw();
		}

		internal void Resize(int width, int height) => GL.Viewport(0, 0, width, height);
	}
}
