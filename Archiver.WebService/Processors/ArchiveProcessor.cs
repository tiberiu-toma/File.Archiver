using System;
using System.IO.Compression;
using System.IO;
using Archiver.WebService.Models;
using Archiver.WebService.DataAccess;

namespace Archiver.WebService.Processors
{
    public static class ArchiveProcessor
    {
        static readonly string path = @"C:\Temp\ArchivedFiles";

        public static void CreateTempArchiveFolder()
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static byte[] ArchiveBytes(string fileName, byte[] buffer)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    var zipEntry = zipArchive.CreateEntry(fileName);
                    using (Stream entryStream = zipEntry.Open())
                    {
                        entryStream.Write(buffer, 0, buffer.Length);
                    }
                }
                return memoryStream.ToArray();
            }
        }

        public static void SaveArchiveToDisk(byte[] zipBytes)
        {
            using (var fileStream = new FileStream($@"{path}\archive.zip", FileMode.OpenOrCreate))
            {
                fileStream.Write(zipBytes, 0, zipBytes.Length);
            }
        }

        public static Stream GetArchive()
        {
            return new FileStream($@"{path}\archive.zip", FileMode.Open, FileAccess.Read);
        }

        public static int SaveArchiveOperationToDb(string fileName, DateTime timeStamp, TimeSpan operationDuration, bool operationStatus)
        {
            var data = new ArchiveOperationModel
            {
                FileName = fileName,
                TimeStamp = timeStamp,
                OperationDuration = operationDuration,
                OperationStatus = operationStatus
            };

            string sql = @"INSERT INTO dbo.Archive (FileName, TimeStamp, OperationDuration, OperationStatus)
                            values (@FileName, @TimeStamp, @OperationDuration, @OperationStatus);";

            return SqlDataAccess.SaveData(sql, data);
        }
    }
}
