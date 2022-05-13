using System;
using System.ComponentModel;
using System.Windows;
using TourPlanner.Models;
using TourPlanner.ViewModels.Commands;

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
            this.ParameterCommand = new ParameterCommand(this);
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
                _description = value;
                OnProbertyChanged(nameof(Transport));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ParameterCommand ParameterCommand { get; set; }
        public void CreateTourButton(object obj)
        {
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
                MessageBox.Show($"Tourname: {Tourname}\nFrom: {From} \nTo: {To} \nDescription: {Description}\nTransport Type: {Transport}");
        }
    }
}
