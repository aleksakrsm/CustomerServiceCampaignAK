using Application;
using Application.Behaviors;
using Application.Features.Campaigns.Commands.CreateCampaign;
using FluentValidation;
using MediatR;
using Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateCampaignCommand).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(CreateCampaignCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



builder.Services.AddControllers();

builder.Services.AddPersistence();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
