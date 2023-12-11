using Microsoft.EntityFrameworkCore;
using Music_Portal.BLL.Infrastructure;
using Music_Portal.BLL.Interfaces;
using Music_Portal.BLL.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddMusicPortalContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ISongService, SongService>();

var app = builder.Build();


//app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();
app.MapControllers();
app.Run();
