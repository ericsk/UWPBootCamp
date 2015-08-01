using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BMI_Calculator
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
    }

    private void OnCalculateButtonClicked(object sender, RoutedEventArgs e)
    {
      double height, weight;

      if (double.TryParse(Height.Text, out height) && double.TryParse(Weight.Text, out weight))
      {
        var heightInM = height / 100.0;

        BMIResult.Text = (weight / (heightInM * heightInM)).ToString();
      }
    }
  }
}
