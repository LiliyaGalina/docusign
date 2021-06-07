using Docusign.API.Models.Utils;
using Docusign.API.Services;
using Docusign.API.Utils;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Docusign.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<DocusignSettings>(Configuration.GetSection("DocuSign"));
            services.Configure<DocusigJWTSettings>(Configuration.GetSection("DocuSignJWT"));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "Docusign.API.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddTransient<EnvelopesApi>(BuildEnvelopesApiClient);
            services.AddTransient<TemplatesApi>(BuildTemplatesApiClient);

            services.AddTransient<DocusignAccountService>();
            services.AddTransient<AuthorizedUserMock>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private EnvelopesApi BuildEnvelopesApiClient(IServiceProvider serviceProvider)
        {
            var accountService = serviceProvider.GetService<DocusignAccountService>();
            var apiClient = accountService.PrepareApiClientConsideringAccount();
            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient);
            return envelopesApi;
        }

        private TemplatesApi BuildTemplatesApiClient(IServiceProvider serviceProvider)
        {
            var accountService = serviceProvider.GetService<DocusignAccountService>();
            var apiClient = accountService.PrepareApiClientConsideringAccount();
            TemplatesApi templatesApi = new TemplatesApi(apiClient);
            return templatesApi;
        }

    }
}
