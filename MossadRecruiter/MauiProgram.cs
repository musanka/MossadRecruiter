using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MossadRecruiter
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Load appsettings.json configuration
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("MossadRecruiter.appsettings.json");

            if (stream == null)
            {
                throw new FileNotFoundException("Could not find embedded resource 'appsettings.json'.");
            }

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
