using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private Tour selectedTourObject;
        public Tour SelectedTourObject
        {
            get { return selectedTourObject; }
            set
            {
                if (value != this.selectedTourObject)
                    selectedTourObject = value;
                this.OnProbertyChanged("SelectedTourObject");
            }
        }

        private ObservableCollection<String> tourObjectCollection;
        public ObservableCollection<String> TourObjectCollection
        {
            get { return tourObjectCollection; }
            set
            {
                if (value != this.tourObjectCollection)
                    tourObjectCollection = value;
                this.OnProbertyChanged("TourObjectCollection");
            }
        }
        private void setTours()
        {
            BussinessLogic bussinessLogic = new BussinessLogic();
            List<string> allTournames = bussinessLogic.SelectAllRoutes();
            allTournames.ForEach(setTourObjectCollection());
        }
        private Action<string> setTourObjectCollection()
        {
            this.tourObjectCollection = new ObservableCollection<String>();
            return f => this.tourObjectCollection.Add(f);
        }

    }
}
