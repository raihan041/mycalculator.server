
using MyCalculator.Data.Models;
using MyCalculator.Data.Repositories;
using MyCalculator.Service.Services;
using Stripe;

namespace MyCalculator.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // See your keys here: https://dashboard.stripe.com/apikeys
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


            //add cors

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy
                                                .AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowAnyOrigin();
                                      });
            });

            // Add services to the container.

            builder.Services.AddControllers();

            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //add dependencies

            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddTransient<IEmailService, EmailService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}