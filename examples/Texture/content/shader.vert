#version 430 compatibility

uniform mat4 modelViewProjection = mat4(1.0);

in vec4 position;
in vec2 texCoord;

out vec2 uv;

void main() 
{
	uv = texCoord;
	vec4 pos = position;
	pos = modelViewProjection * pos;
	gl_Position = pos;
}