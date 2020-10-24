#version 430 core

in vec2 uv;

out vec4 color;

void main() {
	const vec4 color1 = vec4(0.6, 0.0, 0.0, 1.0);
	const vec4 color2 = vec4(0.9, 0.7, 1.0, 0.0);

	float d = distance(uv, vec2(0.5));
	color = mix(color1, color2, smoothstep(0.41, 0.5, d));
//	color = vec4(vec3(smoothstep(0.41, 0.5, d)), 1.0);
}