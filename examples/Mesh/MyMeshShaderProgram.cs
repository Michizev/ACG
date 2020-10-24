using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Example
{
	internal class MyMeshShaderProgram : IDisposable
	{
		public MyMeshShaderProgram()
		{
			_handle = ShaderTools.CreateShaderProgramFromResource("content.shader.vert", "content.shader.frag");
#if SOLUTION
			LocationModelViewProjection = GL.GetUniformLocation(_handle, "modelViewProjection");
			LocationInstancePosition = GL.GetAttribLocation(_handle, "instancePosition");
			LocationPosition = GL.GetAttribLocation(_handle, "position");
			LocationNormal = GL.GetAttribLocation(_handle, "normal");
			LocationTexCoord = GL.GetAttribLocation(_handle, "texCoord");
#endif
		}

#if SOLUTION
		public int LocationModelViewProjection { get; }
		public int LocationInstancePosition { get; }
		public int LocationPosition { get; }
		public int LocationNormal { get; }
		public int LocationTexCoord { get; }
#endif

		public void Activate(Matrix4 modelViewProjection)
		{
			GL.UseProgram(_handle);
#if SOLUTION
			GL.UniformMatrix4(LocationModelViewProjection, false, ref modelViewProjection);
#endif
		}

		public void Dispose()
		{
			GL.DeleteProgram(_handle);
		}

		private readonly int _handle;
	}
}
