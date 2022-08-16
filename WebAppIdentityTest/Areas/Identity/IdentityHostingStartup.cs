using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppIdentityTest.Areas.Identity.Data;
using WebAppIdentityTest.Data;

[assembly: HostingStartup(typeof(WebAppIdentityTest.Areas.Identity.IdentityHostingStartup))]
namespace WebAppIdentityTest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebAppIdentityTestContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebAppIdentityTestContextConnection")));

                services.AddDefaultIdentity<WebAppIdentityTestUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<WebAppIdentityTestContext>();
            });
        }
    }
}