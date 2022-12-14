using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Commands.Providers;
using ProjekatRVA.Core.IConfiguration;
using ProjekatRVA.Core.IRepositories;
using ProjekatRVA.Core.Repositories;
using ProjekatRVA.Data;
using ProjekatRVA.DataInitialization.Interfaces;
using ProjekatRVA.DataInitialization.Providers;
using ProjekatRVA.Invokers;
using ProjekatRVA.Mapping;
using ProjekatRVA.Models.Dto.EventDto;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;
using ProjekatRVA.Service.IServices;
using ProjekatRVA.Service.ServiceProvider;
using ProjekatRVA.Tokens.ITokens;
using ProjekatRVA.Tokens.TokenProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjekatRVA
{
    public class Startup
    {
        string _cors = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<PlannerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PlannerDatabase")));

            //---------------------------------------------------------------------


           




            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true, 
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "http://localhost:44386",
                     IssuerSigningKey = key
                 };
             });
            //--------------------------------------------------------------------------------------


           services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:3001")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserInitialization, UserInitialization>();
            services.AddScoped<ICommand, LoginCommand>();
            services.AddScoped<IReceiver, Receiver>();
            services.AddScoped<IInvoker,Invoker>();
            services.AddScoped<IPlannerRepository,PlannerRepository>();
            services.AddScoped<IPlannerService,PlannerService>();
            services.AddScoped<IEventService,EventService>();
            services.AddScoped<IEventRepository,EventRepository>();
            services.AddScoped<ILoggedUsersRepository,LoggedUsersRepository>();
            services.AddScoped<ILoggedUsersService,LoggedUsersService>();
            services.AddScoped<LoginDto>();
            services.AddScoped<EditPlannerDto>();
            services.AddScoped<AddEventDto>();
            services.AddScoped<EditUserDto>();
            services.AddScoped<RegisterDto>();
            services.AddScoped<SearchDto>();
            services.AddSingleton<ProjekatRVA.Logger.ILogger, ProjekatRVA.Logger.Logger>();
            services.AddScoped<IDataInitialization, ProjekatRVA.DataInitialization.Providers.DataInitialization>();

            //------------------------------------------------
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMappingProfile());
                mc.AddProfile(new PlannerMapingProfile());
                mc.AddProfile(new EventMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            //-----------------------------------------------------


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjekatRVA", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjekatRVA v1"));
            }

            using (var scope = app.ApplicationServices.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<IDataInitialization>().DataInitialization();       
            }

            app.UseRouting();
            app.UseCors(_cors);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
