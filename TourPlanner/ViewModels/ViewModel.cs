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
        private TourPreview selectedTourObject;
        public TourPreview SelectedTourObject
        {
            get => selectedTourObject;
            set
            {
                if (value != this.selectedTourObject)
                    selectedTourObject = value;
                CurTourName = selectedTourObject.tourName;
                CurDescription = "TourID: " + selectedTourObject.tourId;
                this.OnProbertyChanged(nameof(SelectedTourObject));
            }
        }
        private ObservableCollection<TourPreview> tourObjectCollection;
        public ObservableCollection<TourPreview> TourObjectCollection
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
            List<TourPreview> allTournameId = bussinessLogic.SelectAllRoutes();
            allTournameId.ForEach(setTourObjectCollection());
        }
        private Action<TourPreview> setTourObjectCollection()
        {
            this.TourObjectCollection = new ObservableCollection<TourPreview>();
            return f => this.TourObjectCollection.Add(new TourPreview { tourName = f.tourName, tourId = f.tourId });
        }

        public void setImage()
        {
            string image = BussinessLogic.LogicInstance.loadImage("5");
        }
    }
}
