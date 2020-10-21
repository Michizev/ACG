using OpenTK.Graphics.OpenGL4;
using System;

namespace Example
{
	internal class MyPointSpriteShaderProgram : IDisposable
	{
		public MyPointSpriteShaderProgram()
		{
			_handle = ShaderTools.CreateShaderProgramFromRes("content.shader.vert", "content.shader.frag", "content.shader.geom");
			_locTime = GL.GetUniformLocation(_handle, "time");
			_locPointSize = GL.GetUniformLocation(_handle, "pointSize");
			LocationPositionVelocity = GL.GetAttribLocation(_handle, "positionVelocity");
		}

		public int LocationPositionVelocity { get; }

		public void Activate(float time, float pointSize = 0.1f)
		{
			GL.UseProgram(_handle);
			////ATTENTION: always give the time as a float if the uniform in the shader is a float
			GL.Uniform1(_locTime, time);
			GL.Uniform1(_locPointSize, pointSize);
		}

		public void Dispose()
		{
			GL.DeleteProgram(_handle);
		}

		private readonly int _handle;
		private readonly int _locPointSize;
		private readonly int _locTime;
	}
}
