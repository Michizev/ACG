using OpenTK;
using System.Collections.Generic;

namespace Example
{
	class Mesh
	{
		public List<uint> ID = new List<uint>();
		public List<Vector3> Position = new List<Vector3>();
		public List<Vector3> Normal = new List<Vector3>();
		public List<Vector2> TextureCoordinate = new List<Vector2>();
	}
}
