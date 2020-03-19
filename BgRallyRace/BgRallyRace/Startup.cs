namespace BgRallyRace
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using BgRallyRace.Data;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using BgRallyRace.Services;
    using BgRallyRace.Services.Market;
    using BgRallyRace.Services.Training;
    using BgRallyRace.Services.Dismissal;
    using BgRallyRace.Services.Money;
    using BgRallyRace.Services.Admin;
    using Microsoft.AspNetCore.Mvc;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // new ApplicationDbContext().Database.EnsureDeleted();
            // new ApplicationDbContext().Database.EnsureCreated();
            new ApplicationDbContext().Database.Migrate();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // CSRF
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddTransient<IPeople, RallyPilotsServices>();
            services.AddTransient<IPeople, RallyNavigatorsServices>();
            services.AddTransient<IRallyPilotsServices, RallyPilotsServices>();
            services.AddTransient<IRallyNavigatorsServices, RallyNavigatorsServices>();
            services.AddTransient<ICarServices, CarServices>();
            services.AddTransient<IMoneyAccountServices, MoneyAccountServices>();
            services.AddTransient<ITeamServices, TeamServices>();
            services.AddTransient<IOpinionsServices, OpinionsServices>();
            services.AddTransient<IMarketServices, MarketServices>();
            services.AddTransient<ITrainingServices, TrainingServices>();
            services.AddTransient<IDismissalServices, DismissalServices>();
            services.AddTransient<IFinanceServices, FinanceServices>();
            services.AddTransient<ICreateServices, CreateServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
