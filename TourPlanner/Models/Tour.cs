using System;
using System.ComponentModel;
using System.Windows.Controls;
using TourPlanner.ViewModels;

namespace TourPlanner.Models
{
    public class Tour : MainWindowViewModel
    {
        private string _curTourName = string.Empty;
        private string _curDescription = string.Empty;
        public string CurTourName
        {
            get { return _curTourName; }
            set
            {
                if (value != this._curTourName)
                    _curTourName = value;
                this.OnProbertyChanged(CurTourName);
            }
        }
        public string CurDescription
        {
            get { return _curDescription; }
            set
            {
                if (value != this._curDescription)
                    _curDescription = value;
                this.OnProbertyChanged(CurDescription);
            }
        }
        public override string ToString()
        {
            //.FormatString(this string myString) is an extension.
            string returnString = string.Empty;
            if (this._curTourName != string.Empty)
                returnString = String.Format(this._curTourName);
            return returnString;
        }

    }
}
