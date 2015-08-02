using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace ImageViewer
{
  public class MainViewModel : INotifyPropertyChanged
  {
    public ObservableCollection<PhotoItem> Photos { get; private set; }

    public MainViewModel()
    {
      Photos = new ObservableCollection<PhotoItem>();
    }

    public async Task LoadFlickrPhotosAsync(string feedUrl)
    {
      Photos.Clear();

      var filter = new HttpBaseProtocolFilter();
      var client = new HttpClient(filter);
      var respText = await client.GetStringAsync(new Uri(feedUrl));

      // safe check
      respText = respText.Replace("\'", "\\'");

      // process respText
      JsonObject jsonObject = JsonObject.Parse(respText);
      JsonArray jsonArray = jsonObject["items"].GetArray();

      foreach (JsonValue val in jsonArray)
      {
        JsonObject item = val.GetObject();

        Photos.Add(new PhotoItem()
        {
          Title = item["title"].GetString(),
          Link = item["link"].GetString(),
          PhotoUrl = item["media"].GetObject()["m"].GetString()
        });

      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(String propertyName)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (null != handler)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}