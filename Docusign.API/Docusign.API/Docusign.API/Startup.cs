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

            services.AddControllers();

            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddTransient<EnvelopesApi>(BuildEnvelopesApiClient);
            services.AddTransient<TemplatesApi>(BuildTemplatesApiClient);

            services.AddTransient<DocusignAccountService>();

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

            var accessToken = accountService.RequestAccessTokenForDocusign();
            var apiClient = new ApiClient();
            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);
            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient);

            return envelopesApi;
        }

        private TemplatesApi BuildTemplatesApiClient(IServiceProvider serviceProvider)
        {

            var accountService = serviceProvider.GetService<DocusignAccountService>();

            var accessToken = accountService.RequestAccessTokenForDocusign();
            var apiClient = new ApiClient();
            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);
            TemplatesApi templatesApi = new TemplatesApi(apiClient);

            return templatesApi;
        }

    }
}
