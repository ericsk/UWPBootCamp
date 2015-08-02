using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ImageViewer
{
  public sealed partial class MainPage : Page
  {
    private MainViewModel viewModel;

    public MainPage()
    {
      this.InitializeComponent();
      viewModel = new MainViewModel();
      DataContext = viewModel;
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      Url.IsEnabled = false;
      await viewModel.LoadFlickrPhotosAsync(Url.Text);
      Url.IsEnabled = true;
    }

    private async void PhotoGrid_ItemClick(object sender, ItemClickEventArgs e)
    {
      var item = e.ClickedItem as PhotoItem;
      await Launcher.LaunchUriAsync(new Uri(item.Link));
    }
  }
}