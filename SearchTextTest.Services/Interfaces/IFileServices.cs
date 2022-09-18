using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTextTest.Services.Interfaces
{
    public interface IFileServices
    {
        IList<Model.FileServices.File> DoSearch(string text);
    }
}
