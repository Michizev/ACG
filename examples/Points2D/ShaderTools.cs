using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Reflection;

namespace Example
{
	internal static class ShaderTools
	{
		public static int CreateShaderProgram(string vertexShaderSource, string fragmentShaderSource, string geometryShaderSource = "")
		{
			var vertexShader = CreateShader(ShaderType.VertexShader, vertexShaderSource);
			var fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentShaderSource);
			var geometryShader = string.IsNullOrEmpty(geometryShaderSource) ? 0 : CreateShader(ShaderType.GeometryShader, geometryShaderSource);

			var program = GL.CreateProgram();
			GL.AttachShader(program, vertexShader);
			GL.AttachShader(program, fragmentShader);
			if (0 != geometryShader) GL.AttachShader(program, geometryShader);
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

		public static int CreateShaderProgramFromRes(string vertexRes, string fragmentRes, string geometryRes = "")
		{
			var vertexShaderSource = LoadResourceString(vertexRes);
			var fragmentShaderSource = LoadResourceString(fragmentRes);
			var geometryShaderSource = string.IsNullOrEmpty(geometryRes) ? string.Empty : LoadResourceString(geometryRes);
			return CreateShaderProgram(vertexShaderSource, fragmentShaderSource, geometryShaderSource);
		}


		public static string LoadResourceString(string name)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var stream = assembly.GetManifestResourceStream($"{nameof(Example)}.{name}");
			using var streamReader = new StreamReader(stream);
			return streamReader.ReadToEnd();
		}

		private static int CreateShader(ShaderType type, string shaderSource)
		{
			var shader = GL.CreateShader(type);
#if SOLUTION
			shaderSource = shaderSource.Replace("#ifdef " + "SOLUTION", "#if 1");
#endif
			GL.ShaderSource(shader, shaderSource);
			GL.CompileShader(shader);
#if DEBUG
			var log = CheckShader(shader);
			if (!string.IsNullOrEmpty(log))
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
