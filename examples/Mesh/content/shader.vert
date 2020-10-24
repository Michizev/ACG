#version 430 compatibility

uniform mat4 modelViewProjection = mat4(1.0);

in vec3 instancePosition;

in vec4 position;
in vec3 normal;
in vec2 texCoord;

out vec3 n;
out vec2 uv;

void main() 
{
	n = normal;
	uv = texCoord;
	vec4 pos = position;
#ifdef SOLUTION
	pos.xyz += instancePosition;
	pos = modelViewProjection * pos;
#endif
	gl_Position = pos;
}