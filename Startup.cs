using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Bandwidth.Net.Extra;
using Bandwidth.Net.Api;
using Microsoft.Extensions.Configuration;
using System.IO;
using SQLite;
using System.Diagnostics;
using System.Collections;
using Bandwidth.Net;
using SMS_Example_Survey.Controllers;

namespace SMS_Example_Survey
{
    public class Startup
    {
        
        private const string UserId = ""; //{user_id}
        private const string Token = ""; //{token}
        private const string Secret = ""; //{secret}
        
        //public const string CatapultPhone = "+19102262840";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBandwidth(new BandwidthAuthData
            {
                // You can fill these settings via configuration file or environment variables
                UserId = Configuration.GetValue("BANDWIDTH_USER_ID", UserId),
                ApiToken = Configuration.GetValue("BANDWIDTH_API_TOKEN", Token),
                ApiSecret = Configuration.GetValue("BANDWIDTH_API_SECRET", Secret)
            });
            services.AddMvc();
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseBandwidth(new BandwidthOptions
            {
                // Application name on the Bandwidth server
                ApplicationName = "SMS_Example_Survey",

                // Options for allocated phone number
                PhoneNumber = new PhoneNumberOptions
                {
                    Name = "Default",
                    // Remove property LocalNumberQueryForOrder initialization to get toll-free number.
                    LocalNumberQueryForOrder = new LocalNumberQueryForOrder
                    {
                        AreaCode = "910" // Fill query fileds for new phone number. See http://dev.bandwidth.com/ap-docs/methods/availableNumbers/postAvailableNumbersLocal.html
                    }
                },
                // Uncomment this line if you would like reserve a SIP domain on the Bandwidth server (use HttpContext.Items["DomainId"] to get id of reserved domain)
                // DomainName = "my-domain",

                // Messages callback handler
                MessageCallbackDictionary = CallbackEvents.Messages
            });

            // Add MVC support
            app.UseMvc();

            // Handle / as /index.html
            app.UseDefaultFiles();

            // Static files
            app.UseStaticFiles();


            //really need to somehow grab the phonenumber or applicationId here and pass it on
            //the real question is how tf do i get that...

            //complete the rest of program setup and send first question
            //finishSetup();
            FinishStartup.Start();

        }

        

    }
}
