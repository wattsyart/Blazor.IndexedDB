using Blazor.IndexedDB.Server.Data;
using TG.Blazor.IndexedDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddIndexedDB(dbStore =>
{
    dbStore.DbName = "TheFactory";
    dbStore.Version = 1;

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "Employees",
        PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = true },
        Indexes = new List<IndexSpec>
        {
            new IndexSpec{Name="firstName", KeyPath = "firstName", Auto=false},
            new IndexSpec{Name="lastName", KeyPath = "lastName", Auto=false}

        }
    });
    dbStore.Stores.Add(new StoreSchema
    {
        Name = "Outbox",
        PrimaryKey = new IndexSpec { Auto = true }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
