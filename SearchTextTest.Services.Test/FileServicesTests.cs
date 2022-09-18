using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchTextTest.Services.Interfaces;
using SearchTextTest.Services.Services;

namespace SearchTextTest.Services.Test
{
    public class Tests
    {
        private ServiceProvider _Service { get; set; }
        [SetUp]
        public void Setup()
        {
            var myConfiguration = new Dictionary<string, string>
                {
                    {"BaseSearchDirectory", "C:\\Temp\\"},
                };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var services = new ServiceCollection();
            services.AddTransient<IFileServices, FileServices>();
            services.AddSingleton<IConfiguration>(configuration);


            _Service = services.BuildServiceProvider();
        }

        [Test]
        public void DoSearch_TextSearch()
        {
            var fService = _Service.GetService<IFileServices>();
            var result = fService.DoSearch("Cookies");
            if (result.Count > 0)
            {
                Assert.IsTrue(result.Any(x => x.Count > 0));
            }
        }
    }
}