1. compile and run
2. Make an error in the vertex/fragment shader
   - You could also catch the exceptions and print error messages to the Console window if you prefer.
1. Load the shaders from files
   1. `shader.vert` should contain the vertex shader and
   2. `shader.frag` should contain the fragment shader.
   - You can use `System.IO.File.ReadAllText`
   - Note that the path to your shader file can change if you publish your executable.
   - In the solution explorer select the properties of each shader file and set `Copy to Output Directory` to `Copy if newer`.
1. As an alternative you can load the shaders from embedded resources.
   - An embedded resource is any data you want to be linked into your executable (assembly).
   1. In the solution explorer select the properties of each shader file and set `Build Action` to `Embedded Resource`.
   2. Access the resource as a `Stream` with `System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(string name)`
   - With `System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()` you can find out the names of all your embedded resources. You will soon understand the schema.
1. Color the quad with one color.
1. Animate the color over time by using an uniform.
   1. You can measure the elapsed time with a `System.Diagnostics.Stopwatch`
   2. Add a uniform for the time in your fragment shader `uniform float time`.
   2. You can use `GL.Uniform1(int location, float value)` to send the time to the shader program.
   - With `GetUniformLocation(int program, "time")` you will get the uniform location.
