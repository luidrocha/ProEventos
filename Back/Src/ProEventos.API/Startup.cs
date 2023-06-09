using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using ProEventos.Application;
using ProEventos.Application.IContratos;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.IContratos;
using ProEventos.Persistence;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using ProEventos.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProEventos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            // Pega a classe Profile para fazer o automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddDbContext<ProEventosContext>(context => context.UseSqlite(
            // Passa a string de Conexão
            Configuration.GetConnectionString("Default")));

            // Configurações do IDENTITY, quando for criar um usuario vai passar por estas CONFS

            services.AddIdentityCore<User>(options =>

            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            }
            )
            // Configuração baseada na nossa tabela role
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddSignInManager<SignInManager<User>>()
            .AddRoleValidator<RoleValidator<Role>>()
            .AddEntityFrameworkStores<ProEventosContext>()
            // Se não utilizar esta configuração a opção de Reset de senha e geração não funciona
            .AddDefaultTokenProviders();

            // Fim da configuração IdentityCore

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {// Usado para descriptografar a chave 
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                        ValidateIssuer=false,
                        ValidateAudience=false

                    }

                    ) ;




            services.AddControllers() 
                                      // Metodo NATIVO Formata o json retornado de modo que possa gravar as informaçoes do ENUM, não o id e sim a string
                    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                                     // .AddNewtonsoftJson() quebrar o loop infinito de referencias
                    .AddNewtonsoftJson(options => options.SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

                   // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
            // Injeção de Dependencia... Toda vez que encontrar
            // a interface IEventoService, injete o service EventoService
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();

           // Classes de persistencia
            services.AddScoped<IEventoPersistence, EventoPersistence>();
            services.AddScoped<ILotePersistence, LotePersistence>();
            // Classe Generica para crud
            services.AddScoped<IGeralPersistence, GeralPersistence>();
            services.AddScoped<IUserPersist, UserPersist>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Para autorização e autenticação tem que ser nessa ordem
            app.UseAuthentication();
            app.UseAuthorization();

            // permite que requisição Http possa ser executada, liberando o bloqueio
            app.UseCors(acesso => acesso.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());

            // Configura o local para gravação das imagens / arquivos anexos

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Resources")),
                RequestPath=new PathString("/Resources")

            }) ;
                                        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
