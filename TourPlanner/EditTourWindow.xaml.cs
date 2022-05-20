using System;
using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for EditTourWindow.xaml
    /// </summary>
    public partial class EditTourWindow : Window
    {
        public EditTourWindow(string id)
        {
            InitializeComponent();
            var tcv = new TourChangesView();
            tcv.TourId = id;
            this.DataContext = tcv;
            if (tcv.CloseAction == null)
                tcv.CloseAction = new Action(this.Close);
        }
    }
}
