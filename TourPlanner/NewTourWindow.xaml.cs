using System;
using System.ComponentModel;
using System.Windows;
using TourPlanner.ViewModels;

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
            TourChangesView tcv = new TourChangesView();
            DataContext = tcv;
            if (tcv.CloseAction == null)
                tcv.CloseAction = new Action(this.Close);
            Closing += NewTourWindow_Closing; ;
        }
        private void NewTourWindow_Closing(object? sender, CancelEventArgs e)
        {
            this.Hide();
        }
    }
}
