using ChargeMe.BackEnd.Data;
using ChargeMe.BackEnd.lib;
using ChargeMe.BackEnd.Overrides;
using ChargeMe.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var Configuration = builder.Configuration;
builder.Services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();
//builder.Services.AddIdentity<User, IdentityRole>();
builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddRoles<IdentityRole>()
.AddUserManager<UserManager<User>>()
.AddEntityFrameworkStores<DataContext>()
.AddSignInManager();


var app = builder.Build();

DatabaseManager.MigrateDatabase(app);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy=> policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.MapGroup("/api").MapIdentityApiFilterable<User>(new IdentityApiEndpointRouteBuilderOptions()
{
    ExcludeRegisterPost = true,
    ExcludeLoginPost = false,
    ExcludeRefreshPost = false,
    ExcludeConfirmEmailGet = true,
    ExcludeResendConfirmationEmailPost = true,
    ExcludeForgotPasswordPost = true,
    ExcludeResetPasswordPost = true,
    // setting ExcludeManageGroup to false will disable
    // 2FA and both Info Actions
    ExcludeManageGroup = true,
    Exclude2faPost = true,
    ExcludegInfoGet = true,
    ExcludeInfoPost = true,
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
