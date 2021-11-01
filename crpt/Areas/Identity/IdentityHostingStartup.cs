using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(crpt.Areas.Identity.IdentityHostingStartup))]
namespace crpt.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}