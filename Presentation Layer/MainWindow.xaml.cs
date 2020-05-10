using System.Windows;
using System.Windows.Input;
using System.Net;

using Service_Layer;
using Data_Layer;

using GMap.NET.WindowsPresentation;
using GMap.NET.MapProviders;

namespace Presentation_Layer
{
    public partial class MainWindow : Window
    {
        public readonly IService service = new Service();
        public MainWindow()
        {
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            InitializeComponent();
            //service.ParseTweets(path);

        }
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            // choose your provider here
            MapView.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            MapView.MinZoom = 2;
            MapView.MaxZoom = 17;
            // whole world zoom
            MapView.Zoom = 2;
            // lets the map use the mousewheel to zoom
            MapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // lets the user drag the map
            MapView.CanDragMap = true;
            // lets the user drag the map with the left mouse button
            MapView.DragButton = MouseButton.Left;
        }
    }
}
