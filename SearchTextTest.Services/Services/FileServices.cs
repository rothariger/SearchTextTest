using Microsoft.Extensions.Configuration;
using SearchTextTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTextTest.Services.Services
{
    public class FileServices : IFileServices
    {
        private string _baseSearchDirectory { get; set; }
        public FileServices(IConfiguration Configuration)
        {
            _baseSearchDirectory = Configuration["BaseSearchDirectory"];

            if (string.IsNullOrWhiteSpace(_baseSearchDirectory))
            {
                throw new ArgumentNullException("The Configuration for Base Search Directory is empty.");
            }
        }

        public IList<Model.FileServices.File> DoSearch(string text)
        {
            return GetFiles().Select(f =>
                    new Model.FileServices.File
                    {
                        Name = f.Name,
                        Path = f.DirectoryName,
                        Count = File.ReadAllText(f.FullName).Split(text).Count()
                    }).Where(w => w.Count > 0).ToList();
        }

        private IList<FileInfo> GetFiles()
        {
            return System.IO.Directory.GetFiles(_baseSearchDirectory, "*.txt", SearchOption.AllDirectories).Select(f => new FileInfo(f)).ToList();
        }
    }
}
