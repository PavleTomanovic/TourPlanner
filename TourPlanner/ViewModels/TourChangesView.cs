using System;
using System.ComponentModel;
using System.Windows;
using TourPlanner.Models;
using TourPlanner.ViewModels.Commands;
using TourPlanner.BussinesLayer;

namespace TourPlanner.ViewModels
{
    public class TourChangesView //: INotifyPropertyChanged
    {

        private string _tourname;
        private string _from;
        private string _to;
        private string _transport;
        private string _description;

        public TourChangesView()
        {
            this._tourname = "";
            this._from = "";
            this._to = "";
            this._description = "";
            this._transport = "";
            this.ParameterCommand = new ParameterCommand(this);
            this.EditCommand = new EditCommand(this);
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
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description)
                    return;
                _description = value;
                OnProbertyChanged(nameof(Description));
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
        public ParameterCommand ParameterCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public void CreateTourButton(object obj)
        {
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createTour = BussinessLogic.LogicInstance.CreateRoute(Tourname, From, To, Description, Transport);
                if (createTour)
                {
                    TaskSection.createPopup.Close();
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
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createTour = BussinessLogic.LogicInstance.ModifyRoute(From, To, Tourname, Description, Transport, "4");
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
