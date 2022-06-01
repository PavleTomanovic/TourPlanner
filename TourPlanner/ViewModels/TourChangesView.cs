using System;
using System.Windows;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class TourChangesView : ViewModelBase
    {
        public string TourId { get; set; }
        private string _tourname;
        private string _from;
        private string _to;
        private string _transport;
        private string _comment = string.Empty;

        public Action CloseAction { get; set; }
        public TourChangesView()
        {
            _tourname = "";
            _from = "";
            _to = "";
            _comment = "";
            _transport = "";
            NewTourCommand = new TourCommand(this);
            EditTourCommand = new EditTourCommand(this);
        }
        public string Tourname
        {
            get => _tourname;
            set
            {
                if (value == _tourname)
                    return;
                _tourname = value;
                OnPropertyChanged();
            }
        }
        public string From
        {
            get => _from;
            set
            {
                if (value == _from)
                    return;
                _from = value;
                OnPropertyChanged();
            }
        }
        public string To
        {
            get => _to;
            set
            {
                if (value == _to)
                    return;
                _to = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                if (value == _comment)
                    return;
                _comment = value;
                OnPropertyChanged();
            }
        }
        public string Transport
        {
            get => _transport;
            set
            {
                if (value == _transport)
                    return;
                _transport = value;
                OnPropertyChanged();
            }
        }
        //public DelegateCommand ForwardCommand
        public TourCommand NewTourCommand { get; set; }
        public EditTourCommand EditTourCommand { get; set; }
        public ViewModel viewModel { get; set; }
        public void CreateTourButton(object obj)
        {
            UIServices.SetBusyState();
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createTour = BussinessLogic.LogicInstance.CreateRoute(Tourname, From, To, Transport, Comment);
                if (createTour)
                {
                    MessageBox.Show($"Tour: {Tourname} created successfully!", "Tour Created", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();
                    viewModel.updateTourList();
                    viewModel.DataGridDescription?.Reset();
                }
                else
                {
                    MessageBox.Show($"Tour with the name: {Tourname} already exists!", "Tour could not be created", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void EditTourButton(object obj)
        {
            UIServices.SetBusyState();
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool editRoute = BussinessLogic.LogicInstance.ModifyRoute(From, To, Tourname, Transport, Comment, TourId);
                if (editRoute)
                {
                    MessageBox.Show($"Route edited successfully!", "Route Edit", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();
                    viewModel.updateTourList();
                    viewModel.DataGridDescription?.Reset();
                }
                else
                {
                    MessageBox.Show($"Route could not be edited!", "Route Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

    }
}
