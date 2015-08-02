using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace LocalImageViewer
{
  /// <summary>
  /// 可以在本身使用或巡覽至框架內的空白頁面。
  /// </summary>
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();

      Loaded += MainPage_Loaded;
    }

    private async void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
      var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Assets/placeholder.png"));
      using (var stream = await file.OpenAsync(FileAccessMode.Read))
      {
        var bitmap = new BitmapImage();
        await bitmap.SetSourceAsync(stream);
        ImageTarget.Source = bitmap;
      }
    }

    private async void OpenFileButtonClicked(object sender, RoutedEventArgs args)
    {
      FileOpenPicker picker = new FileOpenPicker();
      picker.ViewMode = PickerViewMode.Thumbnail;
      picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

      picker.FileTypeFilter.Add(".jpg");

      StorageFile file = await picker.PickSingleFileAsync();
      if (file != null)
      {
        using (var stream = await file.OpenAsync(FileAccessMode.Read))
        {
          var bitmap = new BitmapImage();
          await bitmap.SetSourceAsync(stream);
          ImageTarget.Source = bitmap;
        }
      }
    }
  }
}
