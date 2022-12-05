using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevTrack.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using StockData.Infrastructure.DbContexts;
using StockData.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
                         .MinimumLevel.Debug()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .ReadFrom.Configuration(builder.Configuration));


try
{

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	var assemblyName = Assembly.GetExecutingAssembly().FullName;

	builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
	builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
	{
		containerBuilder.RegisterModule(new WebModule());
		containerBuilder.RegisterModule(new InfrastructureModule(connectionString,
				assemblyName));
	});

	builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString, m => m.MigrationsAssembly(assemblyName)));
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();
	builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Start-up Failed");
    Console.WriteLine(ex);
}
finally
{
    Log.CloseAndFlush();
}