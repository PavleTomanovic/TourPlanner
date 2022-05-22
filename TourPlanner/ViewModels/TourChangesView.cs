using System;
using System.ComponentModel;
using System.Windows;
using TourPlanner.BussinesLayer;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class TourChangesView : ViewModelBase
    {
        public string TourId { get; set; }
        public string LogID { get; set; }
        private string _tourname;
        private string _from;
        private string _to;
        private string _transport;
        private string _comment;
        private string _logComment;
        private string _difficulty;
        private string _totalTime;
        private string _rating;
        public Action CloseAction { get; set; }
        public TourChangesView()
        {
            _tourname = "";
            _from = "";
            _to = "";
            _comment = "";
            _transport = "";
            _logComment = "";
            _difficulty = "";
            _totalTime = "";
            _rating = "";
            NewTourCommand = new TourCommand(this);
            EditTourCommand = new EditTourCommand(this);
            EditLogCommand = new EditLogCommand(this);
            CreateLogCommand = new CreateLogCommand(this);
        }

        public string Tourname
        {
            get => _tourname;
            set
            {
                if (value == _tourname)
                    return;
                _tourname = value;
                OnPropertyChanged();
            }
        }
        public string From
        {
            get => _from;
            set
            {
                if (value == _from)
                    return;
                _from = value;
                OnPropertyChanged();
            }
        }
        public string To
        {
            get => _to;
            set
            {
                if (value == _to)
                    return;
                _to = value;
                OnPropertyChanged();
            }
        }
        public string Comment
        {
            get => _comment;
            set
            {
                if (value == _comment)
                    return;
                _comment = value;
                OnPropertyChanged();
            }
        }
        public string Transport
        {
            get => _transport;
            set
            {
                if (value == _transport)
                    return;
                _transport = value;
                OnPropertyChanged();
            }
        }

        public string LogComment
        {
            get => _logComment;
            set
            {
                if (value == _logComment)
                    return;
                _logComment = value;
                OnPropertyChanged();
            }
        }

        public string TotalTime
        {
            get => _totalTime;
            set
            {
                if (value == _totalTime)
                    return;
                _totalTime = value;
                OnPropertyChanged();
            }
        }

        public string DifficultyLog
        {
            get => _difficulty;
            set
            {
                if (value == _difficulty)
                    return;
                _difficulty = value;
                OnPropertyChanged();
            }
        }

        public string Rating
        {
            get => _rating;
            set
            {
                if (value == _rating)
                    return;
                _rating = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public TourCommand NewTourCommand { get; set; }
        public EditTourCommand EditTourCommand { get; set; }
        public CreateLogCommand CreateLogCommand { get; set; }
        public EditLogCommand EditLogCommand { get; set; }
        public void CreateTourButton(object obj)
        {
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Comment) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createTour = BussinessLogic.LogicInstance.CreateRoute(Tourname, From, To, Transport, Comment);
                if (createTour)
                {
                    MessageBox.Show($"Tour: {Tourname} created successfully!", "Tour Created", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();
                }
                else
                {
                    MessageBox.Show($"Tour with the name: {Tourname} already exists!", "Tour could not be created", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void EditTourButton(object obj)
        {
            if (string.IsNullOrEmpty(Tourname) || string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Comment) || string.IsNullOrEmpty(Transport))
                MessageBox.Show("Please complete the form");
            else
            {
                bool editRoute = BussinessLogic.LogicInstance.ModifyRoute(From, To, Tourname, Transport, Comment, TourId);
                if (editRoute)
                {
                    MessageBox.Show($"Route edited successfully!", "Route Edit", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();
                }
                else
                {
                    MessageBox.Show($"Route could not be edited!", "Route Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void CreateLogButton(object obj)
        {
            if (string.IsNullOrEmpty(LogComment) || string.IsNullOrEmpty(DifficultyLog) || string.IsNullOrEmpty(TotalTime) || string.IsNullOrEmpty(Rating))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createLog = BussinessLogic.LogicInstance.CreateLog(LogComment, DifficultyLog, TotalTime, Rating, TourId);
                if (createLog)
                {
                    MessageBox.Show($"Log created successfully!", "Log Creation", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();
                }
                else
                {
                    MessageBox.Show($"Log could not be created!", "Log Creation", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void EditLogButton(object obj)
        {
            if (string.IsNullOrEmpty(LogComment) || string.IsNullOrEmpty(DifficultyLog) || string.IsNullOrEmpty(TotalTime) || string.IsNullOrEmpty(Rating))
                MessageBox.Show("Please complete the form");
            else
            {
                bool editLog = BussinessLogic.LogicInstance.ModifyLog(LogComment, DifficultyLog, TotalTime, Rating, LogID);

                if (editLog)
                {
                    MessageBox.Show($"Log edited successfully!", "Log Edit", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();
                }
                else
                {
                    MessageBox.Show($"Log could not be edited!", "Log Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                CloseAction();
            }
        }
    }
}
