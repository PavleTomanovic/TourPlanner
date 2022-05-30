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

    public class ViewModel : ViewModelBase
    {

        private string _curTourId;
        private string _curTourName;
        private string _curImagePath;
        private string _searchText;
        HttpResponseDTO tourDTO = new HttpResponseDTO();
        private DataTable _curDataGrid;
        public OpenWindowCommand OpenWindowCommand { get; set; }
        public OpenEditWindowCommand OpenEditWindowCommand { get; set; }
        public ImportCommand ImportCommand { get; set; }
        public ExportCommand ExportCommand { get; set; }
        public DeleteCommand DeleteCommand { get; set; }
        public OpenInsertLogWindowCommand OpenInsertLogWindowCommand { get; set; }
        public OpenEditLogWindowCommand OpenEditLogWindowCommand { get; set; }
        public DeleteLogCommand DeleteLogCommand { get; set; }
        public TourReportCommand TourReportCommand { get; set; }
        public SummarizeReportCommand SummarizeReportCommand { get; set; }
        public FavoriteNoCommand FavoriteNoCommand { get; set; }
        public FavoriteYesCommand FavoriteYesCommand { get; set; }
        public SearchCommand SearchCommand { get; set; }
        public ViewModel()
        {
            _curTourId = string.Empty;
            _curTourName = "Please choose a Tour";
            _curImagePath = string.Empty;
            _searchText = string.Empty;
            OpenWindowCommand = new OpenWindowCommand(this);
            OpenEditWindowCommand = new OpenEditWindowCommand(this);
            ExportCommand = new ExportCommand(this);
            TourReportCommand = new TourReportCommand(this);
            SummarizeReportCommand = new SummarizeReportCommand(this);
            DeleteCommand = new DeleteCommand(this);
            OpenEditLogWindowCommand = new OpenEditLogWindowCommand(this);
            DeleteLogCommand = new DeleteLogCommand(this);
            ImportCommand = new ImportCommand(this);
            OpenInsertLogWindowCommand = new OpenInsertLogWindowCommand(this);
            FavoriteNoCommand = new FavoriteNoCommand(this);
            FavoriteYesCommand = new FavoriteYesCommand(this);
            SearchCommand = new SearchCommand(this);
            updateTourList();
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

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value != this._searchText)
                    _searchText = value;
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
        private string _curFavorite;

        public string CurFavorite
        {
            get { return _curFavorite; }
            set
            {
                if (value != this._curFavorite)
                    _curFavorite = value;
                OnPropertyChanged();
            }
        }
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
                if (selectedTourObject != null)
                {
                    updateView();
                    updateLog();
                }
                OnPropertyChanged();
            }
        }
        private ObservableCollection<TourPreview> _tourObjectCollection = new ObservableCollection<TourPreview>();
        public ObservableCollection<TourPreview> TourObjectCollection
        {
            get { return _tourObjectCollection; }
            set
            {
                if (value != this._tourObjectCollection)
                    _tourObjectCollection = value;
                OnPropertyChanged();
            }
        }
        TourLogDTO newTourLogDTO = new TourLogDTO();
        public TourLogDTO NewTourLogDTO
        {
            get => newTourLogDTO;
            set
            {
                if (value != this.newTourLogDTO)
                    newTourLogDTO = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<TourLogDTO> _logGrid = new ObservableCollection<TourLogDTO>();
        public ObservableCollection<TourLogDTO> LogGrid
        {
            get => _logGrid;
            set
            {
                if (value != this._logGrid)
                    _logGrid = value;
                OnPropertyChanged();
            }
        }
        public void updateTourList()
        {
            if (TourObjectCollection != null)
                TourObjectCollection.Clear();
            List<TourPreview> allTournameId = new List<TourPreview>();
            allTournameId = BussinessLogic.LogicInstance.SelectTourNameId();
            foreach (var item in allTournameId)
            {
                TourObjectCollection?.Add(item);
                SelectedTourObject = new TourPreview();
            }
        }

        public void updateView()
        {
            //setTours();
            CurTourId = selectedTourObject.tourId;
            var logic = BussinessLogic.LogicInstance;
            tourDTO = logic.SelectAllFromRoute(CurTourId);
            CurTourName = tourDTO.Route.Name;
            CurFrom = tourDTO.Route.From;
            CurTo = tourDTO.Route.To;
            CurTransport = tourDTO.Route.Transport;
            CurComment = tourDTO.Route.Comment;
            CurImagePath = tourDTO.Route.ImageUrl;
            NewTourLogDTO = null;
            if (!string.IsNullOrEmpty(CurTourId))
            {
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

                if (tourDTO.Route.Favorite == "Yes")
                    CurFavorite = @"C:\Taha\Computer Science\4.Semester\SWEN2\TourPlanner\TourPlanner\TourPlanner\Util\star.png";
                else
                    CurFavorite = string.Empty;
            }

        }
        public void updateLog()
        {
            if (LogGrid != null)
                LogGrid.Clear();
            List<TourLogDTO> list = new List<TourLogDTO>();
            list = BussinessLogic.LogicInstance.SelectLogForRoute(CurTourId);
            foreach (var item in list)
            {
                item.DateTime = DateTime.Parse(item.DateTime).ToString("dd.MM.yyyy HH:mm");
                LogGrid?.Add(item);
                NewTourLogDTO = new TourLogDTO();
            }
        }
        /* public void setTours()
        {
            List<TourPreview> allTournameId = new List<TourPreview>();
            allTournameId = BussinessLogic.LogicInstance.SelectTourNameId();
            allTournameId.ForEach(setTourObjectCollection());
        }
        private Action<TourPreview> setTourObjectCollection()
        {
            TourObjectCollection = new ObservableCollection<TourPreview>();
            return f => TourObjectCollection.Add(new TourPreview { tourName = f.tourName, tourId = f.tourId });
        }*/

    }
}
