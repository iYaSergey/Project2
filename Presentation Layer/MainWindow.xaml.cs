using System.Windows;

using Service_Layer;
using Data_Layer;

namespace Presentation_Layer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly IService service = new Service();
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
