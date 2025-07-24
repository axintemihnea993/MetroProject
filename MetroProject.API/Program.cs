using MetroProject.Application.DTOs;
using MetroProject.Application.Repositories;
using MetroProject.Domain;
using MetroProject.Domain.DTOs;
using MetroProject.Domain.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<CheckoutCommandDTO>, TransactionWithPaymentsRepository>();
builder.Services.AddScoped<IRepository<PaymentDTO>, PaymentsRepository>();
builder.Services.AddScoped<IRepository<CustomerDTO>, CustomersRepository>();
builder.Services.AddScoped<IRepository<ArticleDTO>, ArticlesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
