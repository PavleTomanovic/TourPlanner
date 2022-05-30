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
        public EditTourWindow(ViewModel vm, string id, string name, string from, string to, string transport, string comment)
        {
            InitializeComponent();
            var tcv = new TourChangesView();
            tcv.viewModel = vm;
            tcv.TourId = id;
            tcv.Tourname = name;
            tcv.From = from;
            tcv.To = to;
            tcv.Transport = transport;
            tcv.Comment = comment;
            this.DataContext = tcv;
            if (tcv.CloseAction == null)
                tcv.CloseAction = new Action(this.Close);
        }
    }
}
