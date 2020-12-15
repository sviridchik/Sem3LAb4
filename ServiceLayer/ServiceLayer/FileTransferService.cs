using ConfugurationManager;
using DataAccessLayer;
using FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServiceLayer
{
    class FileTransferService : IFileTransferService
    {
        public string content;
        public FileTransferService(string content)
        {
            this.content = content;
        }


        // XmlGeneratorService xmlEntity = new XmlGeneratorService();

        public string GetContent()
        {
            return content;
        }


        public void LogContent()
        {
            Type resFinder = OptionsManager.GetOptions<FinderInfo>();
            FileInfo tempFile = new FileInfo(Path.GetTempFileName());
            var writer = tempFile.AppendText();
            writer.Write(content);
            writer.Close();

            string sourcePathFromStorage = "C:\\" + Logger.GetValueByNameField(resFinder, "SourceDirectory").ToString()
                          .Replace("\"", "").Replace(" ", "");


            FileInfo fileInfo = new FileInfo(Path.Combine(sourcePathFromStorage, "output.xml"));
            if (fileInfo.Exists)
                fileInfo.Delete();

            tempFile.MoveTo(fileInfo.FullName);
        }

    }
}
