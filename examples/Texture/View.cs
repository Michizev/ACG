using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Example
{
	internal class View : IDisposable
	{
		private readonly MyMeshShaderProgram shaderProgram;
		private readonly int texture;
		private readonly VertexArray vertexArray;

		public View()
		{
			shaderProgram = new MyMeshShaderProgram();
			var mesh = MeshTools.LoadFromResource("content.chalet.obj");
			texture = TextureTools.LoadFromResource("content.chalet.jpg");

			// copy position and velocity data to GPU
			vertexArray = new VertexArray(PrimitiveType.Triangles);
			vertexArray.AddIndices(mesh.ID.ToArray());
#if SOLUTION
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
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);
			GL.BindTexture(TextureTarget.Texture2D, texture);
		}

		public void Dispose()
		{
			shaderProgram.Dispose();
		}

		internal void Draw()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			var modelViewProjection = Matrix4.Identity;
#if SOLUTION
			var rotation = Matrix4.CreateRotationX(-0.5f * MathF.PI);
			var projection = Matrix4.CreateOrthographic(4, 4, -4, 4); // use a projection also for handedness change!
			modelViewProjection = projection;
#endif
			shaderProgram.Activate(modelViewProjection);
			vertexArray.Draw();
		}

		internal void Resize(int width, int height) => GL.Viewport(0, 0, width, height);
	}
}
