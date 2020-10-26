# Notes
- We load and render an [obj](https://en.wikipedia.org/wiki/Wavefront_.obj_file) file.
- We use [nuget](https://github.com/chrisjansson/ObjLoader).
- Unused attributes/uniforms give a location of `-1`.
- We will use instancing.


# To do
1. Add mesh attributes to `vertexArray`.
   - We use indexed drawing [glDrawElements](https://www.khronos.org/opengl/wiki/GLAPI/glDrawElements)
   - And store the index data in an `ElementArrayBuffer`.
2. Make the model rotate around the y-axis.
4. Look at the code of `VertexArray.AddAttribute`. I added support for per instance attributes with
   - [VertexAttribDivisor](https://www.khronos.org/opengl/wiki/GLAPI/glVertexAttribDivisor)
   - Drawing is now done with [glDrawElementsInstanced](https://www.khronos.org/opengl/wiki/GLAPI/glDrawElementsInstanced)
   - extension: [ARB_vertex_attrib_binding](https://www.khronos.org/registry/OpenGL/extensions/ARB/ARB_vertex_attrib_binding.txt)
5. Provide 500 values for `instancePosition` and use it in the vertex shader to position each instance.
6. Make the instances rotate individually.
7. Load another obj file (only triangulated models).
8. Create an abstract scene using obj loading and instancing.
