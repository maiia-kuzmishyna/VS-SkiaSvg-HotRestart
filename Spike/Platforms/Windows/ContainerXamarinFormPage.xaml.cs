using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using NavigationEventArgs = Microsoft.UI.Xaml.Navigation.NavigationEventArgs;
using Page = Microsoft.UI.Xaml.Controls.Page;

namespace Spike.Platforms.Windows
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class ContainerXamarinFormPage : Page
   {
      private ContentPage _xamarinFormsPage;
      public ContainerXamarinFormPage()
      {
         InitializeComponent();
      }

      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         base.OnNavigatedTo(e);
         ContentPage basePage = (ContentPage)e.Parameter;
         _xamarinFormsPage = basePage;
         _contentPresenter.Content = basePage.ToPlatform(WinUI.App.MauiContext);
      }

      public ContentPage GetContent()
      {
         return _xamarinFormsPage;
      }
   }
}
