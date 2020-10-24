# Notes
- We load and render an [obj](https://en.wikipedia.org/wiki/Wavefront_.obj_file) file.
- We use [nuget](https://github.com/chrisjansson/ObjLoader).
- Unused attributes/uniforms give a location of `-1`.
- We will use instancing.


# To do
1. Add mesh attributes to `vertexArray`.
1. Make the model fit inside the view-port and rotate it around the y-axis.
1. Look at the code of `VertexArray.AddAttribute`. I added support for per instance attributes.
1. Provide 500 values for `instancePosition` and use it in the vertex shader to position each instance.
1. Load another obj file (only triangulated models).
1. Can you create a wood?
