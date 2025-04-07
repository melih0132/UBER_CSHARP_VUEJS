using Microsoft.EntityFrameworkCore;
using UberApi.Models.DataManager;
using UberApi.Models.EntityFramework;
using UberApi.Models.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using UberApi.Models;

namespace UberApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<S221UberContext>(options =>
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking).UseNpgsql(builder.Configuration.GetConnectionString("S221UberContext")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDataRepository<Client>, ClientManager>();
            builder.Services.AddScoped<IDataRepository<Coursier>, CoursierManager>();
            builder.Services.AddScoped<IDataRepository<Restaurateur>, RestaurateurManager>();
            builder.Services.AddScoped<IDataRepository<Adresse>, AdresseManager>();
            builder.Services.AddScoped<IDataRepository<Vehicule>, VehiculeManager>();
            builder.Services.AddScoped<IDataRepository<Livreur>, LivreurManager>();
            builder.Services.AddScoped<IDataRepository<Etablissement>, EtablissementManager>();
            builder.Services.AddScoped<IDataRepository<Commande>, CommandeManager>();
            builder.Services.AddScoped<IDataRepository<Entretien>, EntretienManager>();
            builder.Services.AddScoped<IDataRepository<Entreprise>, EntrepriseManager>();
            builder.Services.AddScoped<IDataRepository<CarteBancaire>, CarteBancaireManager>();
            builder.Services.AddScoped<IDataRepository<Ville>, VilleManager>();
            builder.Services.AddScoped<IDataRepository<CodePostal>, CodePostalManager>();
            builder.Services.AddScoped<IDataRepository<Pays>, PaysManager>();
            builder.Services.AddScoped<IDataRepository<LieuFavori>, LieuFavoriManager>();
            builder.Services.AddScoped<IDataRepository<Facture>, FactureManager>();
            builder.Services.AddScoped<IDataRepository<Produit>, ProduitManager>();
            builder.Services.AddScoped<IDataRepository<CategoriePrestation>, CategoriePrestationManager>();
            builder.Services.AddScoped<IDataRepository<CategorieProduit>, CategorieProduitManager>();
            builder.Services.AddScoped<IDataRepository<TypePrestation>, TypePrestationManager>();
            builder.Services.AddScoped<IDataRepository<Course>, CourseManager>();
            builder.Services.AddScoped<ICarteBancaireRepository, CarteBancaireManager>();
            builder.Services.AddScoped<ICourseRepository, CourseManager>();
            builder.Services.AddScoped<IDataRepository<Panier>, PanierManager>();
            builder.Services.AddScoped<IPanierRepository, PanierManager>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            builder.Services.AddControllers().AddJsonOptions(options => 
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                     ClockSkew = TimeSpan.Zero
                 };
             });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client"));
                options.AddPolicy("Coursier", policy => policy.RequireRole("Coursier"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}