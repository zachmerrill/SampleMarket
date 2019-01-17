using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

			// services.AddDbContext<SampleMarketDbContext>(options =>
			//       options.UseSqlServer(Configuration.GetConnectionString("SampleMarketDbContext")));

			// Dependency injection of business objects
			// This allows us to use the Moq framework to fake a BO for unit testing
			services.AddTransient<IProductBO, ProductBO>()
				.AddDbContext<SampleMarketDbContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("SampleMarketDbContext")));
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
