using ObjLoader.Loader.Loaders;
using OpenTK;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Example
{
	static class MeshTools
	{
		public static Mesh ToMesh(this LoadResult loadResult)
		{
			var uniqueVertexIDs = new Dictionary<ObjLoader.Loader.Data.Elements.FaceVertex, uint>();
			var mesh = new Mesh();

			foreach (var group in loadResult.Groups)
			{
				foreach (var face in group.Faces)
				{
					//only accept triangles
					if (3 != face.Count) continue;
					for(int i = 0; i < 3; ++i)
					{
						var vertex = face[i];
						if (uniqueVertexIDs.TryGetValue(vertex, out uint index))
						{
							mesh.ID.Add(index);
						}
						else
						{
							uint id = (uint)mesh.Position.Count;
							//add vertex data to mesh
							mesh.ID.Add(id);

							var position = loadResult.Vertices[vertex.VertexIndex - 1];
							mesh.Position.Add(new Vector3(position.X, position.Y, position.Z));
							if (0 != vertex.NormalIndex)
							{
								var normal = loadResult.Normals[vertex.NormalIndex - 1];
								mesh.Normal.Add(new Vector3(normal.X, normal.Y, normal.Z));
							}
							if (0 != vertex.TextureIndex)
							{
								var tex = loadResult.Textures[vertex.TextureIndex - 1];
								mesh.TextureCoordinate.Add(new Vector2(tex.X, tex.Y));
							}
							//new id
							uniqueVertexIDs[vertex] = id;
						}
					}
				}
			}
			return mesh;
		}

		public static Mesh Load(Stream stream)
		{
			var objLoaderFactory = new ObjLoaderFactory();
			var objLoader = objLoaderFactory.Create();

			var loadResult = objLoader.Load(stream);
			return loadResult.ToMesh();
		}

		public static Mesh LoadFromResource(string name)
		{
			var assembly = Assembly.GetExecutingAssembly();
			using var meshStream = assembly.GetManifestResourceStream($"{nameof(Example)}.{name}");
			return Load(meshStream);
		}
	}
}
