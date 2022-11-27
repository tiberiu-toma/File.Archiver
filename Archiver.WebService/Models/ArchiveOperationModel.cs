using System;

namespace Archiver.WebService.Models
{
    public class ArchiveOperationModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public DateTime TimeStamp { get; set; }

        public TimeSpan OperationDuration { get; set; }

        public bool OperationStatus { get; set; }

    }
}
