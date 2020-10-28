using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Example
{
	internal class MyCameraShaderProgram : IDisposable
	{
		public MyCameraShaderProgram()
		{
			_handle = ShaderTools.CreateShaderProgramFromResource("content.shader.vert", "content.shader.frag");
			LocationModelViewProjection = GL.GetUniformLocation(_handle, "modelViewProjection");
			LocationInstancePosition = GL.GetAttribLocation(_handle, "instancePosition");
			LocationPosition = GL.GetAttribLocation(_handle, "position");
			LocationNormal = GL.GetAttribLocation(_handle, "normal");
			LocationTexCoord = GL.GetAttribLocation(_handle, "texCoord");
		}

		public int LocationModelViewProjection { get; }
		public int LocationInstancePosition { get; }
		public int LocationPosition { get; }
		public int LocationNormal { get; }
		public int LocationTexCoord { get; }

		public void Activate(Matrix4 modelViewProjection)
		{
			GL.UseProgram(_handle);
			GL.UniformMatrix4(LocationModelViewProjection, false, ref modelViewProjection);
		}

		public void Dispose()
		{
			GL.DeleteProgram(_handle);
		}

		private readonly int _handle;
	}
}
