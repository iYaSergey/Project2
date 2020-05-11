using System.Windows;
using System.Windows.Input;
using System.Net;
using System.Windows.Documents;
using System.Collections.Generic;

using Service_Layer;
using Data_Layer;

using GMap.NET.WindowsPresentation;
using GMap.NET.MapProviders;
using System.Windows.Controls;

namespace Presentation_Layer
{
    public partial class MainWindow : Window
    {
        string default_path = @"../../../Data Access Layer/Data/";
        public readonly IService service = new Service();
        public MainWindow()
        {
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            InitializeComponent();
        }
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            MapView.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            MapView.MinZoom = 2;
            MapView.MaxZoom = 17;
            MapView.Zoom = 2;
            MapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            MapView.CanDragMap = true;
            MapView.DragButton = MouseButton.Left;
            MapView.ShowCenter = false;
        }
        private void MapList_Loaded(object sender, RoutedEventArgs e)
        {
            SortedList<string, string> files = service.GetFiles(default_path);
            if (files == null)
            {
                MessageBox.Show("Wrong default path.", "Error");
            }
            else
            {
                MapList.ItemsSource = files;
            }
        }
        private void MapList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            object obj = MapList.SelectedItem;
            if (obj != null)
            {
                KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)obj;
                string path = kvp.Value;
                LoadMap(path);
            }
        }
        private void LoadMap(string path)
        {
            Map map = service.GetMap(path);
        }
    }
}
