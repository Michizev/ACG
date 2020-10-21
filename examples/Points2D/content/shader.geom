#version 430 core
layout (points) in;
layout (triangle_strip) out;
layout (max_vertices = 4) out;

uniform float pointSize = 0.1;

out vec2 uv;

void emit(vec2 position, vec2 uv_) {
	gl_Position = vec4(position, 0.0, 1.0);
	uv = uv_;
	EmitVertex();
}


void main() {
	vec2 position = gl_in[0].gl_Position.xy;
	float halveSize = 0.5 * pointSize;
	emit(position + vec2(-halveSize, -halveSize), vec2(0.0, 0.0));
	emit(position + vec2(halveSize, -halveSize), vec2(1.0, 0.0));
	emit(position + vec2(-halveSize, halveSize), vec2(0.0, 1.0));
	emit(position + vec2(halveSize, halveSize), vec2(1.0, 1.0));
}