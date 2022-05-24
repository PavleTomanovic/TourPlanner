using System;
using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for EditLogWindow.xaml
    /// </summary>
    public partial class EditLogWindow : Window
    {
        public EditLogWindow(string id)
        {
            InitializeComponent();
            var tcv = new LogChangesView();
            tcv.LogID = id;
            this.DataContext = tcv;
            if (tcv.CloseAction == null)
                tcv.CloseAction = new Action(this.Close);
        }
    }
}
