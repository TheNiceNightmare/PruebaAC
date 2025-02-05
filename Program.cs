using ConsultorioSeguros.DataAccess;
using ConsultorioSeguros.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure ADO.NET repositories
builder.Services.AddTransient<SeguroRepository>(provider =>
    new SeguroRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<AseguradoRepository>(provider =>
    new AseguradoRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<AseguradoSeguroRepository>(provider =>
    new AseguradoSeguroRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure business services
builder.Services.AddTransient<SegurosService>(provider =>
    new SegurosService(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<AseguradosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
