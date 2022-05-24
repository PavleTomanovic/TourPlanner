using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BussinesLayer;
using TourPlanner.DTO;
using TourPlanner.ViewModels.Commands;

namespace TourPlanner.ViewModels
{
    public class LogChangesView : ViewModelBase
    {
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
        public TourLogDTO TourLogDTO { get; set; }
        public LogChangesView()
        {
            _logComment = "";
            _difficulty = "";
            _totalTime = "";
            _rating = "";
            EditLogCommand = new EditLogCommand(this);
            CreateLogCommand = new CreateLogCommand(this);
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
        public CreateLogCommand CreateLogCommand { get; set; }
        public EditLogCommand EditLogCommand { get; set; }

        public void CreateLogButton(object obj)
        {
            UIServices.SetBusyState();
            if (string.IsNullOrEmpty(LogComment) || string.IsNullOrEmpty(DifficultyLog) || string.IsNullOrEmpty(TotalTime) || string.IsNullOrEmpty(Rating))
                MessageBox.Show("Please complete the form");
            else
            {
                bool createLog = BussinessLogic.LogicInstance.CreateLog(LogComment, DifficultyLog, TotalTime, Rating, TourLogDTO.RouteId);
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
            UIServices.SetBusyState();
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
