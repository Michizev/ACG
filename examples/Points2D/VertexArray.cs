using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Example
{
	internal class VertexArray : IDisposable
	{
		public VertexArray(PrimitiveType type)
		{
			_type = type;
			_vertexArray = GL.GenVertexArray(); // create a vertex array object for interpreting our buffer data (circle points)
		}

		public void AddAttribute<Type>(int attributeLocation, Type[] array, int baseTypeCount, VertexAttribPointerType type) where Type : struct
		{
			if (-1 == attributeLocation) throw new ArgumentException("Invalid attribute location");
			if (0 == array.Length) throw new ArgumentException("Empty attribute array");
			if(0 != _count && array.Length != _count) throw new ArgumentException("Attribute arrays with different sizes given");
			_count = array.Length;

			int byteSize = Marshal.SizeOf(array[0]) * array.Length;
			var buffer = GL.GenBuffer();
			_buffers.Add(buffer);

			GL.BindVertexArray(_vertexArray); // activate vertex array; from now on state is stored;
			GL.BindBuffer(BufferTarget.ArrayBuffer, buffer); // activate buffer
			GL.BufferData(BufferTarget.ArrayBuffer, byteSize, array, BufferUsageHint.StaticDraw); //copy data over to GPU
			GL.EnableVertexAttribArray(attributeLocation); // activate this vertex attribute for the active vertex array
			GL.VertexAttribPointer(attributeLocation, baseTypeCount, type, false, 0, 0); // specify what our buffer contains
			GL.BindVertexArray(0); // deactivate vertex array; state storing is stopped;
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // deactivate buffer; just to be on the cautious side;
			GL.DisableVertexAttribArray(attributeLocation); // cleanup state...
		}

		public void Dispose()
		{
			// for a more correct implementation of Dispose please look MS documentation
			foreach(var buffer in _buffers) GL.DeleteBuffer(buffer);
			GL.DeleteVertexArray(_vertexArray);
		}

		public void Draw()
		{
			GL.BindVertexArray(_vertexArray); // activate vertex array
			GL.DrawArrays(_type, 0, _count); // draw with vertex array data
			 //GL.BindVertexArray(0); // deactivate vertex array would be safer but also slightly slower
		}

		private readonly List<int> _buffers = new List<int>();
		private int _count;
		private readonly PrimitiveType _type;
		private readonly int _vertexArray;
	}
}