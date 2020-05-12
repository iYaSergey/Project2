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
using GMap.NET.WindowsForms.Markers;

namespace Presentation_Layer
{
    public partial class MainWindow : Window
    {
        bool markers_visibility = true;
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
            MapView.Zoom = 3;
            MapView.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            MapView.CanDragMap = true;
            MapView.DragButton = MouseButtons.Left;
            MapView.ShowCenter = false;
            PointLatLng center = new PointLatLng(50, -90);
            MapView.Position = center;
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
            string path = GetPath();
            if (path != null) LoadMap(path);
        }
        private string GetPath()
        {
            string path = null;
            object obj = MapList.SelectedItem;
            if (obj != null)
            {
                KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)obj;
                path = kvp.Value;
            }
            return path;
        }
        private void LoadMap(string path)
        {
            MapView.Overlays.Clear();
            GMapOverlay overlay = new GMapOverlay("Polygons");
            List<GMapPolygon> polygons = service.GetPolygons(path);
            foreach (GMapPolygon polygon in polygons)
            {
                overlay.Polygons.Add(polygon);
            }

            foreach (Tweet tweet in service.GetTweets())
            {
                GMarkerGoogleType type;
                if (tweet.Weight > 0) type = GMarkerGoogleType.green_small;
                else if (tweet.Weight < 0) type = GMarkerGoogleType.red_small;
                else type = GMarkerGoogleType.yellow_small;
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(tweet.Location[0], tweet.Location[1]), type);
                marker.ToolTipText = tweet.Text;
                marker.IsVisible = markers_visibility;
                overlay.Markers.Add(marker);
            }

            #region PRESS F
            Brush background = new SolidBrush(Color.Transparent);
            Font font = new Font("Arial", 9, System.Drawing.FontStyle.Bold);
            background.GetType();
            GMarkerGoogle KOSTblL_MARKER = new GMarkerGoogle(new PointLatLng(53.64443431, 23.85695636), GMarkerGoogleType.yellow);
            KOSTblL_MARKER.ToolTip = new GMapToolTip(KOSTblL_MARKER);
            KOSTblL_MARKER.ToolTip.Stroke.Color = Color.FromArgb(0, 255, 255, 255);
            KOSTblL_MARKER.ToolTip.Font = font;
            KOSTblL_MARKER.ToolTip.Fill = background;
            KOSTblL_MARKER.ToolTipText = "ПОСТАВЬТЕ 9 ЗА ПЕРВУЮ ЛАБУ, ПОЖАЛУЙСТА :(";
            overlay.Markers.Add(KOSTblL_MARKER);
            #endregion

            MapView.Overlays.Add(overlay);
            overlay.IsVisibile = false;
            overlay.IsVisibile = true;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string path = GetPath();
            if (path != null) LoadMap(path);
        }
        private void ShowMarkers(object sender, RoutedEventArgs e)
        {
            markers_visibility = true;
            if (MapView.Overlays != null && MapView.Overlays.Count != 0)
                foreach (GMarkerGoogle marker in MapView.Overlays[0].Markers)
                {
                    marker.IsVisible = true;
                }
        }
        private void DeleteMarkers(object sender, RoutedEventArgs e)
        {
            markers_visibility = false;
            if (MapView.Overlays != null && MapView.Overlays.Count != 0)
                foreach (GMarkerGoogle marker in MapView.Overlays[0].Markers)
                {
                    marker.IsVisible = false;
                }
        }
    }
}