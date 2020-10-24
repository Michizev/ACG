#version 430 core
uniform sampler2D texDiffuse;

in vec2 uv;

out vec4 color;

void main() 
{
	//use uv as color
	color = vec4(uv, 0.0, 1.0);
#ifdef SOLUTION
	color = texture(texDiffuse, uv);
#endif
}