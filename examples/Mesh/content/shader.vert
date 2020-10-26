#version 430 compatibility
uniform float time;
uniform mat4 modelViewProjection = mat4(1.0);

in vec3 instancePosition;

in vec4 position;
in vec3 normal;
in vec2 texCoord;

#ifdef SOLUTION
vec3 rotateY(vec3 pos, float angle)
{
	float c = cos(angle);
	float s = sin(angle);
	mat3 rot = mat3(vec3(c, 0, s), vec3(0, 1, 0), vec3(-s, 0, c));
	return rot * pos;
}
#endif

out vec3 n;
out vec2 uv;

void main() 
{
	n = normal;
	uv = texCoord;
	vec4 pos = position;
#ifdef SOLUTION
	pos.xyz = rotateY(position.xyz, time * mod(instancePosition.x + instancePosition.y + instancePosition.z, 4.0)) + instancePosition;
	pos = modelViewProjection * pos;
#endif
	gl_Position = pos;
}