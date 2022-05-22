using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;
using TourPlanner.Models;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    // View Model

    public class ViewModel : ViewModelBase
    {

        private string _curTourId = string.Empty;
        private string _curTourName = "Please choose a Tour";
        private string _curImagePath = string.Empty;
        HttpResponseDTO tourDTO = new HttpResponseDTO();
        private DataTable _curDataGrid;
        public OpenWindowCommand OpenWindowCommand = new OpenWindowCommand();
        public OpenEditWindowCommand OpenEditWindowCommand { get; set; }
        public ImportCommand ImportCommand = new ImportCommand();
        public ExportCommand ExportCommand { get; set; }
        public DeleteCommand DeleteCommand { get; set; }
        public OpenInsertLogWindowCommand OpenInsertLogWindowCommand = new OpenInsertLogWindowCommand();
        public OpenEditLogWindowCommand OpenEditLogWindowCommand { get; set; }
        public DeleteLogCommand DeleteLogCommand { get; set; }
        public TourReportCommand TourReportCommand { get; set; }
        public SummarizeReportCommand SummarizeReportCommand { get; set; }

        public ViewModel()
        {
            OpenEditWindowCommand = new OpenEditWindowCommand(this);
            ExportCommand = new ExportCommand(this);
            TourReportCommand = new TourReportCommand(this);
            SummarizeReportCommand = new SummarizeReportCommand(this);
            DeleteCommand = new DeleteCommand(this);
            OpenEditLogWindowCommand = new OpenEditLogWindowCommand();
            DeleteLogCommand = new DeleteLogCommand();
            setTours(); //versuche es mit Button für update view : Notiz für Taha
        }
        public string CurTourId
        {
            get { return _curTourId; }
            set
            {
                if (value != this._curTourId)
                    _curTourId = value;
                OnPropertyChanged();
            }
        }

        public string CurTourName
        {
            get { return _curTourName; }
            set
            {
                if (value != this._curTourName)
                    _curTourName = value;
                OnPropertyChanged();
            }
        }
        public string CurFrom;
        public string CurTo;
        public string CurTransport;
        public string CurComment;

        public DataTable DataGridDescription
        {
            get { return _curDataGrid; }
            set
            {
                if (value != this._curDataGrid)
                    _curDataGrid = value;
                OnPropertyChanged();
            }
        }
        public string CurImagePath
        {
            get { return _curImagePath; }
            set
            {
                if (value != this._curImagePath)
                    _curImagePath = value;
                OnPropertyChanged();
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
                updateView();
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }
        public void setTours()
        {
            List<TourPreview> allTournameId = BussinessLogic.LogicInstance.SelectTourNameId();
            allTournameId.ForEach(setTourObjectCollection());
        }
        private Action<TourPreview> setTourObjectCollection()
        {
            tourObjectCollection = new ObservableCollection<TourPreview>();
            return f => tourObjectCollection.Add(new TourPreview { tourName = f.tourName, tourId = f.tourId });
        }

        private void updateView()
        {
            setTours();
            CurTourId = selectedTourObject.tourId;
            var logic = BussinessLogic.LogicInstance;
            tourDTO = logic.SelectAllFromRoute(CurTourId);
            CurTourName = tourDTO.Route.Name;
            CurFrom = tourDTO.Route.From;
            CurTo = tourDTO.Route.To;
            CurTransport = tourDTO.Route.Transport;
            CurComment = tourDTO.Route.Comment;
            CurImagePath = tourDTO.Route.ImageUrl;

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
            myDataRow["Two"] = CurFrom;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "To: ";
            myDataRow["Two"] = CurTo;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "Transport: ";
            myDataRow["Two"] = CurTransport;
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
            myDataRow["Two"] = CurComment;
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "Popularity: ";
            myDataRow["Two"] = logic.CheckRoutePopularity(CurTourId);
            custTable.Rows.Add(myDataRow);
            myDataRow = custTable.NewRow();
            myDataRow["One"] = "Child friendly: ";
            myDataRow["Two"] = logic.CheckRouteChildFriendliness(CurTourId);
            custTable.Rows.Add(myDataRow);
            DataGridDescription = custTable;

            /*            var found = TourObjectCollection.FirstOrDefault(x => x.tourId == CurTourId);
                        if (found != null)
                            found.tourName = CurTourName;*/
        }
    }
}
