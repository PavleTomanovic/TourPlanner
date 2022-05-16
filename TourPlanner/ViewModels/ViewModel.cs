using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TourPlanner.BussinesLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    // View Model

    public class ViewModel : MainWindowViewModel
    {
        public ViewModel()
        {
            setTours();
        }
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
        /* public override string ToString()
         {
             string returnString = string.Empty;
             if (this._curTourName != string.Empty)
                 returnString = String.Format(this._curTourName);
             return returnString;
         }*/
        private string selectedTourObject;
        public string SelectedTourObject
        {
            get => selectedTourObject;
            set
            {
                if (value != this.selectedTourObject)
                    selectedTourObject = value;
                this.OnProbertyChanged("SelectedTourObject");
            }
        }
        private ObservableCollection<string> tourObjectCollection;
        public ObservableCollection<string> TourObjectCollection
        {
            get { return tourObjectCollection; }
            set
            {
                if (value != this.tourObjectCollection)
                    tourObjectCollection = value;
                this.OnProbertyChanged("TourObjectCollection");
            }
        }
        public void setTours()
        {
            BussinessLogic bussinessLogic = new BussinessLogic();
            List<string> allTournames = bussinessLogic.SelectAllRoutes();
            allTournames.ForEach(setTourObjectCollection());
        }
        private Action<string> setTourObjectCollection()
        {
            this.tourObjectCollection = new ObservableCollection<string>();
            return f => this.tourObjectCollection.Add(f);
        }

    }
}
