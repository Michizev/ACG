# To do
1. Generate individual sizes for each point on the CPU and use them in your shader
1. Create individual colors for each point on the GPU.
1. Change the vertex shader so that points that leave the screen reenter on the opposite side (like in the game Asteroids)
1. Change the vertex shader so that points bounce off the window borders.
   - Tip: You could use a repeated tent function, like `abs(mod(position + 3, 4) - 2) - 1)`