using System;
using System.Windows;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class LogChangesView : ViewModelBase
    {
        private string _tourID;
        private string _logID;
        private string _logComment;
        private string _difficulty;
        private string _totalTime;
        private string _rating;
        public CreateLogCommand CreateLogCommand { get; set; }
        public EditLogCommand EditLogCommand { get; set; }
        public Action CloseAction { get; set; }
        public LogChangesView()
        {
            EditLogCommand = new EditLogCommand(this);
            CreateLogCommand = new CreateLogCommand(this);
        }
        public string TourID
        {
            get => _tourID;
            set
            {
                if (value == _tourID)
                    return;
                _tourID = value;
                OnPropertyChanged();
            }
        }
        public string LogID
        {
            get => _logID;
            set
            {
                if (value == _logID)
                    return;
                _logID = value;
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

        public string Difficulty
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


        public void CreateLogButton(object obj)
        {
            UIServices.SetBusyState();
            if (string.IsNullOrEmpty(LogComment) || string.IsNullOrEmpty(Difficulty) || string.IsNullOrEmpty(TotalTime) || string.IsNullOrEmpty(Rating))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createLog = BussinessLogic.LogicInstance.CreateLog(LogComment, Difficulty, TotalTime, Rating, TourID);
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

        public void EditLogButton()
        {
            UIServices.SetBusyState();
            if (string.IsNullOrEmpty(LogComment) || string.IsNullOrEmpty(Difficulty) || string.IsNullOrEmpty(TotalTime) || string.IsNullOrEmpty(Rating))
                MessageBox.Show("Please complete the form");
            else
            {
                bool editLog = BussinessLogic.LogicInstance.ModifyLog(LogComment, Difficulty, TotalTime, Rating, LogID, TourID);
                if (editLog)
                {
                    MessageBox.Show($"Log edited successfully!", "Log Edit", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseAction();
                }
                else
                {
                    MessageBox.Show($"Log could not be edited!", "Log Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
