using Microsoft.Maui.Controls;

namespace Spike;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
