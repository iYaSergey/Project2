using System.Windows;

using Service_Layer;
using Data_Layer;

namespace Presentation_Layer
{
    public partial class MainWindow : Window
    {
        string path;

        public readonly IService service = new Service();
        public MainWindow()
        {
            InitializeComponent();
            //service.ParseTweets(path);

        }
    }
}
