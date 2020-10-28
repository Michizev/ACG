using OpenTK;
using OpenTK.Input;
using System;

namespace Example
{
	internal class Program
	{
		private static void Main()
		{
			var window = new GameWindow
			{
				WindowState = WindowState.Maximized
			};

			window.KeyDown += (_, a) =>
			{
				switch (a.Key)
				{
					case Key.Escape: window.Close(); break;
				}
			};

			using var view = new View();

			window.MouseMove += (s, e) =>
			{
				if (ButtonState.Pressed == e.Mouse.LeftButton)
				{
					view.OrbitingCamera.Azimuth += 300 * e.XDelta / (float)window.Width;
					view.OrbitingCamera.Elevation += 300 * e.YDelta / (float)window.Height;
				}
			};
			window.MouseWheel += (s, e) => view.OrbitingCamera.Distance *= (float)MathF.Pow(1.05f, e.DeltaPrecise);

			window.RenderFrame += (__, _) => view.Draw();
			window.RenderFrame += (__, _) => window.SwapBuffers();
			window.Resize += (__, _) => view.Resize(window.Width, window.Height);
			window.Run();
		}
	}
}