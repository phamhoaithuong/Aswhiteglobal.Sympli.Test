using Aswhiteglobal.Sympli.HostingExtensions;
namespace Aswhiteglobal.Sympli;
public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder
            .ConfigureServices()
            .ConfigurePipeline();

        app.Run();
    }
}