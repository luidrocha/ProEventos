using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProEventos.Application;
using ProEventos.Application.IContratos;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.IContratos;
using ProEventos.Persistence;
using AutoMapper;



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
            services.AddControllers() // .AddNewtonsoftJson() quebrar o loop infinito de referencias
                    .AddNewtonsoftJson(x => x.SerializerSettings
                    .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           
            // Injeção de Dependencia... Toda vez que encontrar
            // a interface IEventoService, injete o service EventoService
            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<ILoteService, LoteService>();

            services.AddScoped<IEventoPersistence, EventoPersistence>();
            services.AddScoped<ILotePersistence, LotePersistence>();
            services.AddScoped<IGeralPersistence, GeralPersistence>();
            
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

            app.UseAuthorization();

            // permite que requisição Http possa ser executada, liberando o bloqueio
            app.UseCors(acesso => acesso.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod());
                                        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
