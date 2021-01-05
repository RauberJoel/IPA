using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScrumRetroApp.Blazor.Services;

namespace ScrumRetroApp.Blazor
{
	public class Startup
	{
		#region Properties
		public IConfiguration Configuration { get; }
		#endregion

		#region Constructors
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}
		#endregion

		#region Publics
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
			        .AddAzureAD(options => this.Configuration.Bind("AzureAd", options));

			services.AddControllersWithViews(options =>
			                                 {
				                                 AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
				                                                              .RequireAuthenticatedUser()
				                                                              .Build();
				                                 options.Filters.Add(new AuthorizeFilter(policy));
			                                 });

			services.AddRazorPages();
			services.AddServerSideBlazor();
			services.AddHttpContextAccessor();
			services.AddSingleton<HttpClient>();
			services.AddSignalR();
			services.AddResponseCompression(opts =>
			                                {
				                                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
				                                                                                              new[] { "application/octet-stream" });
			                                });
			services.AddScoped<ISession, Session>();
			services.AddHttpClient<IBenutzerService, BenutzerService>(client => client.BaseAddress = new Uri("https://localhost:5001/"));

			//Wird zu einem späteren Zeitpunkt umgesetzt, nur übergangsmässig drin
			//services.AddSingleton<CircuitHandler, TrackingCircuitHandler>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseResponseCompression();
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			                 {
				                 endpoints.MapControllers();
				                 endpoints.MapBlazorHub();
				                 endpoints.MapFallbackToPage("/_Host");
			                 });
		}
		#endregion
	}
}