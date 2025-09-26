using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Data;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? "Server=(localdb)\\MSSQLLocalDB;Database=AttendanceDB;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));
builder.Services.AddRazorPages();

var app = builder.Build();

// --- Auto-create DB/tables on startup (dev convenience) ---
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Use EnsureCreated for simple prototypes; it creates DB & tables from DbContext
        db.Database.EnsureCreated();

        // Optional: log database name to console for diagnosis
        var cs = db.Database.GetDbConnection().ConnectionString;
        Console.WriteLine("Connected DB: " + db.Database.GetDbConnection().Database);
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB startup error: " + ex.Message);
    }
}
// ---------------------------------------------------------

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
