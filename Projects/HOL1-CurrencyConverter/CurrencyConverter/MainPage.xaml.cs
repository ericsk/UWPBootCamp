namespace CurrencyConverter
{
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Controls;

  public sealed partial class MainPage : Page
  {
    const double RATE = 31.0;

    public MainPage()
    {
      InitializeComponent();
    }

    private void OnConvertButtonClick(object sender, RoutedEventArgs e)
    {
      double usd;

      if (double.TryParse(USD.Text, out usd))
      {
        TWD.Text = (usd * RATE).ToString();
      }
    }
  }
}