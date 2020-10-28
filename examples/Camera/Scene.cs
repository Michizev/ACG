using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Example
{
	class Scene : IDisposable
	{
		public Scene()
		{
			shaderProgram = new MyCameraShaderProgram();
			axis = CoordinateSystemAxis();
			vertexArray = MeshInstances(shaderProgram);
		}

		public void Dispose()
		{
			shaderProgram.Dispose();
		}

		public void Draw(Matrix4 modelViewProjection)
		{
			shaderProgram.Activate(modelViewProjection);
			vertexArray.Draw();
			GL.UseProgram(0);
			OpenTK.Graphics.OpenGL.GL.LoadMatrix(ref modelViewProjection);
			axis.Draw();
		}

		private readonly MyCameraShaderProgram shaderProgram;
		private readonly VertexArray axis;
		private readonly VertexArray vertexArray;

		private static VertexArray MeshInstances(MyCameraShaderProgram shaderProgram)
		{
			var mesh = MeshTools.LoadFromResource("content.suzanne.obj");
			var vertexArray = new VertexArray(PrimitiveType.Triangles);
			vertexArray.AddIndices(mesh.ID.ToArray());

			const int count = 100;
			var rnd = new Random();
			float RndRange(float min, float max) => min + (max - min) * (float)rnd.NextDouble();
			var position = new Vector3[count];
			for (int i = 0; i < count; ++i)
			{
				position[i] = new Vector3(RndRange(-8f, 8f), RndRange(-.5f, .5f), RndRange(-40f, -0f));
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
			return vertexArray;
		}

		private static VertexArray CoordinateSystemAxis()
		{
			var vertexArray = new VertexArray(PrimitiveType.Lines);
			var coords = new Vector3[] { Vector3.Zero, Vector3.UnitX, Vector3.Zero, Vector3.UnitY, Vector3.Zero, Vector3.UnitZ };
			var colors = new Vector3[] { Vector3.UnitX, Vector3.UnitX, Vector3.UnitY, Vector3.UnitY, Vector3.UnitZ, Vector3.UnitZ };
			vertexArray.AddAttribute(0, coords, 3, VertexAttribPointerType.Float);
			vertexArray.AddAttribute(3, colors, 3, VertexAttribPointerType.Float);
			return vertexArray;
		}
	}
}
