using System.Text;
using Loan_Management_System.Configurations;
using Loan_Management_System.Data;
using Loan_Management_System.Models;
using Loan_Management_System.Repository;
using Loan_Management_System.Services;
using Loan_Management_System.Services.Email;
using Loan_Management_System.Services.Payment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
namespace Loan_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            QuestPDF.Settings.License = LicenseType.Community;

            // Add services to the container.
            builder.Services.AddDbContext<LoanDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection")));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ILoanOfficerRepository, LoanOfficerRepository>();
            builder.Services.AddScoped<ILoanOfficerService, LoanOfficerService>();
            builder.Services.AddScoped<ILoanSchemeRepository, LoanSchemeRepository>();
            builder.Services.AddScoped<ILoanSchemeService, LoanSchemeService>();
            builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
            builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IPaymentGatewayService, DummyPaymentGatewayService>();
            builder.Services.AddScoped<IRepaymentService, RepaymentService>();
            builder.Services.AddScoped<IRepaymentRepository, RepaymentRepository>();
            builder.Services.AddScoped<INpaRepository, NpaRepository>();
            builder.Services.AddScoped<INpaService, NpaService>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddScoped<Services.Report.ReportGeneratorService>();
            builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
            builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();

            // Email settings
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            // Repos & services
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            // Email sender
            builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();

            




            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });










            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();



            // Swagger configuration to show enum names in the swagger UI/alo authorize button in corner
            builder.Services.AddSwaggerGen(options =>
            {
                // Basic API Info
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Smart Library App / Loan API",
                    Description = "API documentation for Smart Library and Loan Management System"
                });

                // Show enum names instead of numeric values
                options.UseInlineDefinitionsForEnums();

                // JWT Bearer Authentication Setup
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter JWT Bearer token only",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
            });





            //jwt ke liye
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
    };

    // 👇 Global Unauthorized Response
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse(); // Suppress default 401 response

            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                success = false,
                message = "Unauthorized access. Please provide a valid JWT token."
            });

            return context.Response.WriteAsync(result);
        }
    };
});







            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();
            //    emailSender.SendEmailAsync("rameshvishwakarma715@gmail.com", "Test Email", "Hello Ramesh, test works!");
            //}
            //Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //this  below added so that when refresh then also token persist rahe
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan_Management");
                        options.EnablePersistAuthorization();
                    }
                    );
            }
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
