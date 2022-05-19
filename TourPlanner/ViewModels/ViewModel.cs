using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;
using TourPlanner.Models;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    // View Model

    public class ViewModel : MainWindowViewModel
    {
        private string _curTourId = string.Empty;
        private string _curTourName = "Please choose a Tour";
        private string _curDescription = string.Empty;
        private string _curImagePath = string.Empty;
        private DataTable _curDataGrid;
        public OpenWindowCommand OpenWindowCommand { get; set; }
        public OpenEditWindowCommand OpenEditWindowCommand { get; set; }

        public ViewModel()
        {
            setTours();
            OpenWindowCommand = new OpenWindowCommand(CurTourId);
            OpenEditWindowCommand = new OpenEditWindowCommand(CurTourId);
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
        public DataTable DataGridDescription
        {
            get { return _curDataGrid; }
            set
            {
                if (value != this._curDataGrid)
                    _curDataGrid = value;
                this.OnProbertyChanged(nameof(DataGridDescription));
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
        HttpResponseDTO tourDTO = new HttpResponseDTO();
        private TourPreview selectedTourObject;
        public TourPreview SelectedTourObject
        {
            get => selectedTourObject;
            set
            {
                if (value != this.selectedTourObject)
                    selectedTourObject = value;
                updateView();
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
        public void updateView()
        {
            CurTourName = selectedTourObject.tourName;
            CurTourId = selectedTourObject.tourId;
            tourDTO = BussinessLogic.LogicInstance.SelectAllFromRoute(CurTourId);
            CurImagePath = BussinessLogic.LogicInstance.loadImage(CurTourId);
            /*
               CurDescription = "From: " + httpResponseDTO.Route.From
               + "\nTo: " + httpResponseDTO.Route.To
               + "\nTransport Type: " + httpResponseDTO.Route.Transport
               + "\nDistance: " + httpResponseDTO.Route.Distance
               + "\nTime: " + httpResponseDTO.Route.Time
               + "\nComment: " + httpResponseDTO.Route.Comment;
               Ich wollte das die Daten gut lesbar sind: eine Tabelle war das erste was mir einfiel...
            */
            DataTable custTable = new DataTable();
            DataColumn dtColumn;
            DataRow myDataRow;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "One";
            dtColumn.ReadOnly = true;
            custTable.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Two";
            dtColumn.ReadOnly = true;
            custTable.Columns.Add(dtColumn);

            myDataRow = custTable.NewRow();
            myDataRow["One"] = "From: ";
            myDataRow["Two"] = tourDTO.Route.From;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "To: ";
            myDataRow["Two"] = tourDTO.Route.To;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "Transport: ";
            myDataRow["Two"] = tourDTO.Route.Transport;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "Distance: ";
            myDataRow["Two"] = tourDTO.Route.Distance;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "Time: ";
            myDataRow["Two"] = tourDTO.Route.Time;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "Comment: ";
            myDataRow["Two"] = tourDTO.Route.Comment;
            custTable.Rows.Add(myDataRow);
            DataGridDescription = custTable;

        }
    }
}
