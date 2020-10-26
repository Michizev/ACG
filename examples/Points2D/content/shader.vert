#version 430 core
uniform float time;

in vec2 position;
in vec2 velocity;

void main() {
	vec2 newPos = position + time * velocity;
#ifdef SOLUTION
	newPos = abs(mod(newPos + 3.0, 4.0) - 2.0) - 1.0;
#endif
	gl_Position = vec4(newPos, 0.0, 1.0);
}