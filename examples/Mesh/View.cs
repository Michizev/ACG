﻿using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;

namespace Example
{
	internal class View : IDisposable
	{
		private readonly MyMeshShaderProgram shaderProgram;
		private readonly Stopwatch time;
		private readonly VertexArray vertexArray;

		public View()
		{
			shaderProgram = new MyMeshShaderProgram();
			var mesh = MeshTools.LoadFromResource("content.suzanne.obj");

			// copy data to GPU
			vertexArray = new VertexArray(PrimitiveType.Triangles);
			vertexArray.AddIndices(mesh.ID.ToArray());
#if SOLUTION
			const int count = 500;
			var rnd = new Random(12);
			float Rnd01() => (float)rnd.NextDouble();
			float RndCoord() => (Rnd01() - 0.5f) * 10.0f;
			var position = new Vector3[count];
			for (int i = 0; i < count; ++i)
			{
				position[i] = new Vector3(RndCoord(), RndCoord(), RndCoord());
			}
			vertexArray.AddAttribute(shaderProgram.LocationInstancePosition, position, 3, VertexAttribPointerType.Float, true);

			vertexArray.AddAttribute(shaderProgram.LocationPosition, mesh.Position.ToArray(), 3, VertexAttribPointerType.Float);
			if (mesh.Normal.Count > 0 && -1 != shaderProgram.LocationNormal)
			{
				vertexArray.AddAttribute(shaderProgram.LocationNormal, mesh.Normal.ToArray(), 3, VertexAttribPointerType.Float);
			}
			if (mesh.TextureCoordinate.Count > 0 && -1 != shaderProgram.LocationTexCoord)
			{
				vertexArray.AddAttribute(shaderProgram.LocationTexCoord, mesh.TextureCoordinate.ToArray(), 2, VertexAttribPointerType.Float);
			}
#endif
			time = Stopwatch.StartNew();

			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);
		}

		public void Dispose()
		{
			shaderProgram.Dispose();
		}

		internal void Draw()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			var currentTime = (float)time.Elapsed.TotalSeconds;
			var modelViewProjection = Matrix4.CreateOrthographic(15, 15, -15, 15); // use a projection also for handedness change!
#if SOLUTION
			var rotation = Matrix4.CreateRotationY(currentTime);
			modelViewProjection = rotation * modelViewProjection;
#endif
			shaderProgram.Activate(modelViewProjection, currentTime);
			vertexArray.Draw();
		}

		internal void Resize(int width, int height) => GL.Viewport(0, 0, width, height);
	}
}
