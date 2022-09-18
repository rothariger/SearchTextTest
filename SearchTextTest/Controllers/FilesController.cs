using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchTextTest.Services.Interfaces;

namespace SearchTextTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IFileServices _FileServices { get; set; }

        public FilesController(IFileServices FileServices)
        {
            _FileServices = FileServices;
        }

        [HttpGet("{text}")]
        public IList<Model.FileServices.File> Get(string text)
        {
            return _FileServices.DoSearch(text);
        }
    }
}
