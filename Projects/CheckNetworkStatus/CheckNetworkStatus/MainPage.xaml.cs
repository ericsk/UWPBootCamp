using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CheckNetworkStatus
{
  public sealed partial class MainPage : Page
  {
    bool registered = false;

    public MainPage()
    {
      this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      foreach (var task in BackgroundTaskRegistration.AllTasks)
      {
        if (task.Value.Name == "NetworkTask")
        {
          task.Value.Completed += new BackgroundTaskCompletedEventHandler(Task_Completed);
          registered = true;
          break;
        }
      }

      Register.IsEnabled = !registered;
      Unregister.IsEnabled = registered;
    }


    private void Register_Click(object sender, RoutedEventArgs e)
    {
      var builder = new BackgroundTaskBuilder();
      builder.Name = "NetworkTask";
      builder.TaskEntryPoint = "Tasks.NetworkTask";
      builder.SetTrigger(new SystemTrigger(SystemTriggerType.NetworkStateChange, false));

      BackgroundTaskRegistration task = builder.Register();

      Register.IsEnabled = false;
      Unregister.IsEnabled = true;

      task.Completed += new BackgroundTaskCompletedEventHandler(Task_Completed);
    }

    private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
    {
      var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

      Debug.WriteLine(settings.Values["NetworkStatus"]);
    }

    private void Unregister_Click(object sender, RoutedEventArgs e)
    {
      foreach (var cur in BackgroundTaskRegistration.AllTasks)
      {
        if (cur.Value.Name == "NetworkTask")
        {
          cur.Value.Unregister(true);
          Register.IsEnabled = true;
          Unregister.IsEnabled = false;
          break;
        }
      }
    }
  }
}
