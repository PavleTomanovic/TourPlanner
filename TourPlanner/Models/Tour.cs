using System.ComponentModel;

namespace TourPlanner.Models
{
    public class Tour : INotifyPropertyChanged
    {
        private string _tourname;
        private string _from;
        private string _to;
        private string _destination;
        private string _description;
        public Tour()
        {
            this._tourname = "";
            this._from = "";
            this._to = "";
            this._destination = "";
            this._description = "";
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
        public string Destination
        {
            get => _destination;
            set
            {
                if (value == _destination)
                    return;
                _destination = value;
                OnProbertyChanged(nameof(Destination));
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
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
