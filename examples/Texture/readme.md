# Notes
- We load and render an [obj](https://en.wikipedia.org/wiki/Wavefront_.obj_file) file.
- We use [nuget](https://github.com/chrisjansson/ObjLoader).
- Unused attributes/uniforms give a location of `-1`.
- We will use instancing.


# To do
1. Add mesh attributes to `vertexArray`.
2. Make the model fit inside the view-port and rotate it around the y-axis.
   - Use a perspective or orthogonal projection.
3. Look at the code of `VertexArray.AddAttribute`. I added support for per instance attributes.
4. Provide 500 values for `instancePosition` and use it in the vertex shader to position each instance.
5. Make the instances rotate individually.
6. let heads orbit each other -> binary head systems
7. solar systems of heads
8. Galaxies of solar systems
9. The big head-bang theory...
10. Load another obj file (only triangulated models).
11. Can you create a wood?
