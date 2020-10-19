The goal of this first exercise is to get a first impression of how to work with shaders, uniforms and the interplay of different shader stages (vertex and fragment).

1. Compile and run
1. Try to move each vertex of the quad individually in the vertex shader.
1. Color the quad with one color.
1. Make an error in the vertex/fragment shader
   - Find and inspect the code that is responsible for throwing the exception.
   - You could also catch the exceptions and print error messages to the Console window if you prefer.
1. Working with shaders as strings is only feasible for small shaders, so we will implement loading the shaders from files
   1. `shader.vert` should contain the vertex shader and
   1. `shader.frag` should contain the fragment shader.
   - You can use `System.IO.File.ReadAllText`
   - Move the shaders in a project subdirectory, for instance `content/shader`.
   - Note that the path to your shader file can change if you publish your executable.
   - In the solution explorer select the properties of each shader file and set `Copy to Output Directory` to `Copy if newer`. This will assure that the shader files are always available next to the executable.
   - If you want to work with shader files in Visual Studio, you probably want syntax coloring, intelli sense and early error detection. A Visual Studio extensions for this is `GLSL language integration` [github](https://github.com/danielscherzer/GLSL).
1. As an alternative you can load the shaders from embedded resources.
   - An embedded resource is any data you want to be linked into your executable (assembly).
   1. In the solution explorer select the properties of each shader file and set `Build Action` to `Embedded Resource`.
   1. Access the resource as a `Stream` with `System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(string name)`
   - With `System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames()` you can find out the names of all your embedded resources. You will soon understand the schema.
1. Animate the color/positions over time by using an uniform.
   1. You can measure the elapsed time with a `System.Diagnostics.Stopwatch`
   1. Add a uniform for the time in your fragment shader `uniform float time`.
   1. You can use `GL.Uniform1(int location, float value)` to send the time to the shader program.
   - With `GetUniformLocation(int program, "time")` you will get the uniform location.
   - Note that `stopwatch.Elapsed.TotalSeconds` returns a `double`. Convert it to `float` because although OpenGL supports `double` most consumer graphics cards do not support it.
