using SearchTextTest.Services.Interfaces;
using SearchTextTest.Services.Services;

namespace SearchTextTest.Infrastructure
{
    public static class DI
    {
        public static void InitializeInjections(this IServiceCollection services)
        {
            services.AddTransient<IFileServices, FileServices>();
        }
    }
}
