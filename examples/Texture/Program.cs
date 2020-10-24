using OpenTK;
using OpenTK.Input;

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

			window.RenderFrame += (__, _) => view.Draw();
			window.RenderFrame += (__, _) => window.SwapBuffers();
			window.Resize += (__, _) => view.Resize(window.Width, window.Height);
			window.Run();
		}
	}
}