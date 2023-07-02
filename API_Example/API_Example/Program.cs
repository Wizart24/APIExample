global using API_Example.Models;
global using API_Example.Services.CharacterService;
global using API_Example.Dtos.Character;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using API_Example.Data;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using API_Example.Services.FightService;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using API_Example.Services.WeaponService;

namespace API_Example
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = """Standart Authorization header using the Bearer scheme. Example: "bearer {token}" """,
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				c.OperationFilter<SecurityRequirementsOperationFilter>();
			});
			builder.Services.AddAutoMapper(typeof(Program).Assembly);
			builder.Services.AddScoped<ICharacterService, CharacterService>();
			builder.Services.AddScoped<IAuthRepository, AuthRepository>();
			builder.Services.AddScoped<IWeaponService, WeaponService>();
			builder.Services.AddScoped<IFightService, FightService>();
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
							.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
			builder.Services.AddHttpContextAccessor();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}