using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace Example
{
	internal class VertexArrayVector4 : IDisposable
	{
		private readonly int _buffer;
		private readonly int _count;
		private readonly PrimitiveType _type;
		private readonly int _vertexArray;

		public VertexArrayVector4(PrimitiveType type, Vector4[] points, int attributeLocation)
		{
			_type = type;

			_count = points.Length;

			_vertexArray = GL.GenVertexArray(); // create a vertex array object for interpreting our buffer data (circle points)
			_buffer = GL.GenBuffer();
			GL.BindVertexArray(_vertexArray); // activate vertex array; from now on state is stored;

			int byteSize = Vector4.SizeInBytes * points.Length; // calculate size in bytes of point array
			GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer); // activate buffer
			GL.BufferData(BufferTarget.ArrayBuffer, byteSize, points, BufferUsageHint.StaticDraw); //copy data over

			GL.EnableVertexAttribArray(attributeLocation); // activate this vertex attribute for the active vertex array
			GL.VertexAttribPointer(attributeLocation, 4, VertexAttribPointerType.Float, false, 0, 0); // specify what our buffer contains
			GL.BindVertexArray(0); // deactivate vertex array; state storing is stopped;
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // deactivate buffer; just to be on the cautious side;
		}

		public void Dispose()
		{
			// for a more correct implementation of Dispose please look MS documentation
			GL.DeleteBuffer(_buffer);
			GL.DeleteVertexArray(_vertexArray);
		}

		public void Draw()
		{
			GL.BindVertexArray(_vertexArray); // activate vertex array
			GL.DrawArrays(_type, 0, _count); // draw with vertex array data
											 //GL.BindVertexArray(0); // deactivate vertex array would be safer but also slightly slower
		}
	}
}