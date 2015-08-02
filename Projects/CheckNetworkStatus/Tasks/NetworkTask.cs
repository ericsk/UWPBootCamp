using System.Diagnostics;
using Windows.ApplicationModel.Background;
using Windows.Networking.Connectivity;

namespace Tasks
{
  public sealed class NetworkTask : IBackgroundTask
  {
    BackgroundTaskDeferral _deferral = null;

    public void Run(IBackgroundTaskInstance taskInstance)
    {
      Debug.WriteLine("Background " + taskInstance.Task.Name + " Starting...");

      taskInstance.Canceled += new BackgroundTaskCanceledEventHandler(OnCanceled);

      _deferral = taskInstance.GetDeferral();

      ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

      var settings = Windows.Storage.ApplicationData.Current.LocalSettings;
      var status = "";
      switch (InternetConnectionProfile.GetNetworkConnectivityLevel())
      {
        case NetworkConnectivityLevel.None:
          status = "無連線";
          break;
        case NetworkConnectivityLevel.LocalAccess:
          status = "本地連線";
          break;
        case NetworkConnectivityLevel.ConstrainedInternetAccess:
          status = "受限的連線";
          break;
        case NetworkConnectivityLevel.InternetAccess:
          status = "網際網路連線";
          break;
      }
      settings.Values["NetworkStatus"] = status;

      _deferral.Complete();
    }

    private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
    {
      Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
    }
  }
}
