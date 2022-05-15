using System;
using System.ComponentModel;
using TourPlanner.ViewModels;

namespace TourPlanner.Models
{
    public class Tour : MainWindowViewModel
    {
        private string _curTourName = string.Empty;
        public string CurTourName
        {
            get { return _curTourName; }
            set
            {
                if (value != this._curTourName)
                    _curTourName = value;
                this.OnProbertyChanged("CurTourName");
            }
        }
        private string _curDescription = string.Empty;
        public string CurDescription
        {
            get { return _curDescription; }
            set
            {
                if (value != this._curDescription)
                    _curDescription = value;
                this.OnProbertyChanged("CurDescription");
            }
        }
    }
}
