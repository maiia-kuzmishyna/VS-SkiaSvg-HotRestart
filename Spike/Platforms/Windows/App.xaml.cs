using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.UI.Xaml;
using Window = Microsoft.UI.Xaml.Window;
using Frame = Microsoft.UI.Xaml.Controls.Frame;
using Microsoft.UI.Xaml.Navigation;
using Spike.Platforms.Windows;
using LaunchActivatedEventArgs = Microsoft.UI.Xaml.LaunchActivatedEventArgs;
using System;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.LifecycleEvents;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Spike.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
   /// <summary>
   /// Initializes the singleton application object.  This is the first line of authored code
   /// executed, and as such is the logical equivalent of main() or WinMain().
   /// </summary>
   public App()
   {
      InitializeComponent();
   }

   public static MauiContext MauiContext { get; private set; }

   protected override async void OnLaunched(LaunchActivatedEventArgs args)
   {
      var window = new Window();
      
      var rootFrame = window.Content as Frame;

      if (rootFrame == null)
      {
         rootFrame = new Frame();
         rootFrame.NavigationFailed += OnNavigationFailed;
         window.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
         var builder = MauiApp.CreateBuilder();
         builder
            .UseMauiEmbedding<Microsoft.Maui.Controls.Application>()
            .ConfigureFonts(fonts =>
            {
               fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
               fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

         var mauiApp = builder.Build();
         MauiContext = new MauiContext(mauiApp.Services);

         var page = new MainPage();
         rootFrame.Navigate(typeof(ContainerXamarinFormPage), page);
         Microsoft.Maui.Controls.Application.Current = new Spike.App();
         Microsoft.Maui.Controls.Application.Current!.MainPage = page;
      }

      window.Activated += Window_Activated;

      window.Activate();
   }

   private void Window_Activated(object sender, WindowActivatedEventArgs args)
   {
      WindowStateManager.Default.OnActivated((Window)sender, args);
   }

   private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
   {
      throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
   }

   protected override MauiApp CreateMauiApp()
   {
      return MauiProgram.CreateMauiApp();
   }
}