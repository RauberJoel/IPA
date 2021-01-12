using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScrumRetroApp.Api;
using ScrumRetroApp.API.Services;

namespace ScrumRetroApp.API
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
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddSwaggerGen();

			services.AddSingleton<IDatabaseService>(x => new DatabaseService(Settings.DbConnection));
			services.AddScoped<IBenutzerService, BenutzerService>();
			services.AddScoped<ITFSService, TFSService>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment()) app.UseDeveloperExceptionPage();

			app.UseHttpsRedirection();

			app.UseSwagger();

			app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1"); });
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			                 {
				                 endpoints.MapControllers();
			                 });

			app.UseStatusCodePages();
		}
		#endregion
	}
}