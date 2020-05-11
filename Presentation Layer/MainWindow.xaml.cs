using System.Windows;
using System.Windows.Input;
using System.Net;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Controls;
using System.IO;
using System.Windows.Ink;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Data.SQLite;

using Service_Layer;
using Data_Layer;

using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Presentation_Layer
{
    public partial class MainWindow : Window
    {
        string default_path = @"../../../../Data Access Layer/Data/";
        public readonly IService service = new Service();
        public MainWindow()
        {
            InitializeComponent();
            GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            GMapProvider.WebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
        }
        private void MapView_Load(object sender, System.EventArgs e)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            MapView.MapProvider = OpenStreetMapProvider.Instance;
            MapView.MinZoom = 2;
            MapView.MaxZoom = 17;
            MapView.Zoom = 2;
            MapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            MapView.CanDragMap = true;
            MapView.DragButton = MouseButtons.Left;
            MapView.ShowCenter = false;
        }
        private void MapList_Loaded(object sender, RoutedEventArgs e)
        {
            SortedList<string, string> files = service.GetFiles(default_path);
            if (files == null)
            {
                System.Windows.MessageBox.Show("Wrong default path.", "Error");
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
            GMapOverlay overlay = new GMapOverlay("Polygons");
            List<GMapPolygon> polygons = service.GetPolygons(path);
            foreach (GMapPolygon polygon in polygons)
            {
                overlay.Polygons.Add(polygon);
            }
            MapView.Overlays.Add(overlay);
        }

    }
}
