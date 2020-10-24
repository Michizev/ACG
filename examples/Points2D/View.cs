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
		private readonly VertexArray vertexArray;

		public View()
		{
			shaderPointSprite = new MyPointSpriteShaderProgram();

			const int pointCount = 500;
			//generate starting positions and velocity array on CPU
			var rnd = new Random(12);
			float Rnd01() => (float)rnd.NextDouble();
			float RndCoord() => (Rnd01() - 0.5f) * 2.0f;
			float RndSpeed() => (Rnd01() - 0.5f) * 0.1f;
			var position = new Vector2[pointCount];
			var velocity = new Vector2[pointCount];
			for (int i = 0; i < pointCount; ++i)
			{
				position[i] = new Vector2(RndCoord(), RndCoord());
				velocity[i] = new Vector2(RndSpeed(), RndSpeed());
			}

			// copy position and velocity data to GPU
			vertexArray = new VertexArray(PrimitiveType.Points);
			vertexArray.AddAttribute(shaderPointSprite.LocationPosition, position, 2, VertexAttribPointerType.Float);
			vertexArray.AddAttribute(shaderPointSprite.LocationVelocity, velocity, 2, VertexAttribPointerType.Float);

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
			shaderPointSprite.Activate((float)time.Elapsed.TotalSeconds, 0.07f);
			vertexArray.Draw();
		}

		internal void Resize(int width, int height) => GL.Viewport(0, 0, width, height);
	}
}
