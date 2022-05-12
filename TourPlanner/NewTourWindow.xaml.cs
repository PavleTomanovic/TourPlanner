using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TourPlanner
{
    /// <summary>
    /// Interaktionslogik für NewTourWindow.xaml
    /// </summary>
    public partial class NewTourWindow : Window
    {
        public NewTourWindow()
        {
            InitializeComponent();
            this.DataContext = new TourChangesView();
        }

        TourChangesView TourChangesView { get; set; }
        private string eName;
        private string eFrom;
        private string eDestination;
        private string eDescription;
        private void CreateTourButton(object sender, RoutedEventArgs e)
        {
            eName = TourChangesView?.Tourname;
            MessageBox.Show(eName);

        }

    }
}
