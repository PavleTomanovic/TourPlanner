using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner
{
    internal class TourChangesView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TourChangesView()
        {
        }
        private string tourname = "";
        private string from = "";
        private string destination = "";
        private string description = "" +
            "";


        public void OnProbertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public string Tourname
        {
            get => tourname;
            set
            {
                if (value == tourname)
                    return;
                tourname = value;
                OnProbertyChanged(nameof(Tourname));
            }
        }
        public string From
        {
            get => from;
            set
            {
                if (value == from)
                    return;
                from = value;
                OnProbertyChanged(nameof(From));
            }
        }
        public string Destination
        {
            get => destination;
            set
            {
                if (value == destination)
                    return;
                destination = value;
                OnProbertyChanged(nameof(Destination));
            }
        }
        public string Description
        {
            get => description;
            set
            {
                if (value == description)
                    return;
                description = value;
                OnProbertyChanged(nameof(Description));
            }
        }

    }
}
