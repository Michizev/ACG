#version 430 core
uniform float time;

in vec4 positionVelocity;

out vec3 pos;
void main() {
	vec2 position = positionVelocity.xy;
	vec2 velocity = positionVelocity.zw;
	vec2 newPos = position + time * velocity;
#ifdef SOLUTION
	newPos = abs(mod(newPos + 3.0, 4.0) - 2.0) - 1.0;
#endif
	gl_Position = vec4(newPos, 0.0, 1.0);
}