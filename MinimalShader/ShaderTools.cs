using OpenTK.Graphics.OpenGL4;

namespace Example
{
	static class ShaderTools
	{
		public static int CreateShaderProgram(string vertexShaderSource, string fragmentShaderSource)
		{
			var vertexShader = CreateShader(ShaderType.VertexShader, vertexShaderSource);
			var fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentShaderSource);
			var program = GL.CreateProgram();
			GL.AttachShader(program, vertexShader);
			GL.AttachShader(program, fragmentShader);
			GL.LinkProgram(program);
			GL.DeleteShader(vertexShader);
			GL.DeleteShader(fragmentShader);
#if DEBUG
			var log = CheckShaderProgram(program);
			if (!string.IsNullOrEmpty(log))
			{
				throw new ShaderProgramException(log);
			}
#endif
			return program;
		}

		private static int CreateShader(ShaderType type, string shaderSource)
		{
			var shader = GL.CreateShader(type);
			GL.ShaderSource(shader, shaderSource);
			GL.CompileShader(shader);
#if DEBUG
			var log = CheckShader(shader);
			if(!string.IsNullOrEmpty(log))
			{
				throw new ShaderException(type, log);
			}
#endif
			return shader;
		}

		private static string CheckShader(int shader)
		{
			GL.GetShader(shader, ShaderParameter.CompileStatus, out int status_code);
			if (1 == status_code)
			{
				return string.Empty;
			}
			else
			{
				return GL.GetShaderInfoLog(shader);
			}
		}
		private static string CheckShaderProgram(int shaderProgram)
		{
			GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out int status_code);
			if (1 == status_code)
			{
				return string.Empty;
			}
			else
			{
				return GL.GetProgramInfoLog(shaderProgram);
			}
		}
	}
}
