using System;
using System.Runtime.Serialization;

namespace Example
{
	[Serializable]
	internal class ShaderProgramException : Exception
	{
		public ShaderProgramException()
		{
		}

		public ShaderProgramException(string message) : base(message)
		{
		}

		public ShaderProgramException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ShaderProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}