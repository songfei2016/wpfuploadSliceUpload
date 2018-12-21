using System;
using System.Windows.Input;

namespace St.Model
{
    public class ResourceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }
        public DateTime AddTime { get; set; }

        public DateTime? UploadTime { get; set; }

        public bool UploadStatus { get; set; }

        public int UserId { get; set; }

        public int? SchoolId { get; set; }

        public string Description { get; set; }

        public ICommand GotoUpload { get; set; }

        public ICommand CoverUpload { get; set; }

        public ICommand GotoDetail { get; set; }

        public ICommand ReGotoUpload { get; set; }

        //public ICommand SortingCommand { get; set; }
    }
}
