var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.Strict;
        options.HttpOnly = HttpOnlyPolicy.None;
        options.Secure = CookieSecurePolicy.Always;
    });

    builder.Services.AddSession(options =>
    {
        options.Cookie.Name = ".SessionCookie";
        options.IdleTimeout = TimeSpan.FromMinutes(double.Parse(builder.Configuration.GetValue<string>("Session:TimeoutInMinutes")));
    });


    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllersWithViews();

    builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

    builder.Services.AddHttpClient<IFileHelper, FileHelper>(httpClient =>
    {
        httpClient.BaseAddress = new Uri(uriString: builder.Configuration.GetValue<string>("WebService:BaseUri"));
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
             AuthenticationSchemes.Basic.ToString(),
             Convert.ToBase64String(Encoding.ASCII.GetBytes($"{CipherHelper.Decrypt(builder.Configuration.GetValue<string>("WebService:Username"))}:{CipherHelper.Decrypt(builder.Configuration.GetValue<string>("WebService:Password"))}"))
             );
    });

    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCookiePolicy();
    app.UseSession();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });

    app.Run();

}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}
