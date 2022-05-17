using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
            setImage();
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
                this.OnProbertyChanged(nameof(CurTourName));
            }
        }
        public string CurDescription
        {
            get { return _curDescription; }
            set
            {
                if (value != this._curDescription)
                    _curDescription = value;
                this.OnProbertyChanged(nameof(CurDescription));
            }
        }
        private string selectedTourObject;
        public string SelectedTourObject
        {
            get => selectedTourObject;
            set
            {
                if (value != this.selectedTourObject)
                    selectedTourObject = value;
                CurTourName = selectedTourObject;
                this.OnProbertyChanged(nameof(SelectedTourObject));
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
                this.OnProbertyChanged(nameof(TourObjectCollection));
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
            this.TourObjectCollection = new ObservableCollection<string>();
            return f => this.TourObjectCollection.Add(f);
        }

        public void setImage()
        {
            string image = BussinessLogic.LogicInstance.loadImage("5");
        }
    }
}
