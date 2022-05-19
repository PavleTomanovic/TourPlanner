using System;
using System.ComponentModel;
using System.Windows;
using TourPlanner.Models;
using TourPlanner.ViewModels.Commands;
using TourPlanner.BussinesLayer;
using System.Windows.Input;

namespace TourPlanner.ViewModels
{
    public class TourChangesView
    {
        public string _tourId;
        private string _tourname;
        private string _from;
        private string _to;
        private string _transport;
        private string _comment;
        public TourChangesView()
        {
            _tourname = "";
            _from = "";
            _to = "";
            _comment = "";
            _transport = "";
            NewTourCommand = new CreateTourCommand(this);
            EditTourCommand = new EditTourCommand(this);
        }
        public void OnProbertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public string Tourname
        {
            get => _tourname;
            set
            {
                if (value == _tourname)
                    return;
                _tourname = value;
                OnProbertyChanged(nameof(Tourname));
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
                OnProbertyChanged(nameof(From));
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
                OnProbertyChanged(nameof(To));
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
                OnProbertyChanged(nameof(Comment));
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
                OnProbertyChanged(nameof(Transport));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public CreateTourCommand NewTourCommand { get; set; }
        public EditTourCommand EditTourCommand { get; set; }
        public void CreateTourButton(object obj)
        {
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Comment) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createTour = BussinessLogic.LogicInstance.CreateRoute(Tourname, From, To, Transport, Comment);
                if (createTour)
                {
                    OpenWindowCommand.createPopup.Close();
                    MessageBox.Show($"Tour: {Tourname} created successfully!", "Tour Created");
                }
                else
                {
                    MessageBox.Show($"Tour with the name: {Tourname} already exists!", "Tour could not be created", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void EditTourButton(object obj)
        {
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Comment) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createTour = BussinessLogic.LogicInstance.ModifyRoute(From, To, Tourname, Transport, Comment, "4");
                if (createTour)
                {
                    TaskSection.editPopup.Close();
                    MessageBox.Show($"Tour: {Tourname} edited successfully!", "Tour Edit");
                }
                else
                {
                    MessageBox.Show($"Tour with the name: {Tourname} already exists!", "Tour could not be edited", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
