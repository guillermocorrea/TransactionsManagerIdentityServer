using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TransactionsManagerIdentityServer
{
    public class Startup
    {
        public static string[] Args { get; set; } = new string[] { };
        private ILogger logger;
        private ILoggerFactory loggerFactory;
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Startup.Args);

            Configuration = builder.Build();
            this.loggerFactory = loggerFactory;
            this.loggerFactory.AddConsole(LogLevel.Information);
            this.loggerFactory.AddDebug();
            this.logger = this.loggerFactory.CreateLogger("Startup");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            Config config = new Config(Configuration);
            services.AddIdentityServer()
                .AddInMemoryClients(config.GetClients())
                .AddInMemoryIdentityResources(config.GetIdentityResources())
                .AddInMemoryApiResources(config.GetApiResources())
                .AddTestUsers(config.GetUsers())
                .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
