using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner.BussinesLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    // View Model

    public class ViewModel : MainWindowViewModel
    {
        private string _curTourId = string.Empty;
        private string _curTourName = string.Empty;
        private string _curDescription = string.Empty;
        private string _curImagePath = string.Empty;
        public ViewModel()
        {
            setTours();
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            //setImage();
=======
>>>>>>> Stashed changes
        }
        public string CurTourId
        {
            get { return _curTourId; }
            set
            {
                if (value != this._curTourId)
                    _curTourId = value;
                this.OnProbertyChanged(nameof(CurTourId));
            }
        }
=======
        }
        public string CurTourId
        {
            get { return _curTourId; }
            set
            {
                if (value != this._curTourId)
                    _curTourId = value;
                this.OnProbertyChanged(nameof(CurTourId));
            }
        }
>>>>>>> Stashed changes
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
        public string CurImagePath
        {
            get { return _curImagePath; }
            set
            {
                if (value != this._curImagePath)
                    _curImagePath = value;
                this.OnProbertyChanged(nameof(CurImagePath));
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
                CurTourId = selectedTourObject.tourId;
                CurDescription = "TourID: " + CurTourId;
                CurImagePath = setImage();
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
            List<TourPreview> allTournameId = bussinessLogic.SelectTourNameId();
            allTournameId.ForEach(setTourObjectCollection());
        }
        private Action<TourPreview> setTourObjectCollection()
        {
            this.TourObjectCollection = new ObservableCollection<TourPreview>();
            return f => this.TourObjectCollection.Add(new TourPreview { tourName = f.tourName, tourId = f.tourId });
        }

        public string setImage()
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            string image = BussinessLogic.LogicInstance.loadImage(selectedTourObject.tourId.ToString());
=======
            return BussinessLogic.LogicInstance.loadImage(selectedTourObject.tourId);
>>>>>>> Stashed changes
=======
            return BussinessLogic.LogicInstance.loadImage(selectedTourObject.tourId);
>>>>>>> Stashed changes
        }
    }
}
