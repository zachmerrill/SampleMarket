using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleMarket.Business;
using Microsoft.EntityFrameworkCore;
using SampleMarket.Data;

namespace SampleMarket
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			// Dependency injection of business objects
			// This allows us to use the Moq framework to fake the business objects for unit testing
			// Without actually reaching out to the database
			services.AddDbContext<SampleMarketDbContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("SampleMarketDbContext")))
					.AddTransient<IProductBO, ProductBO>()
					.AddTransient<ICartBO, CartBO>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
