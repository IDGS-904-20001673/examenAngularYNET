
using Examen.BUSSINESS.services.interfaces;
using Examen.BUSSINESS.Dependencies;

using Examen.BUSSINESS.services;
using Examen.ENTITY;
using Microsoft.EntityFrameworkCore;
using System;
using Examen.DATA.Repositories.interfaces;
using Examen.DATA.Repositories;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.InyecDependencies(builder.Configuration);

builder.Services.AddCors(options => {
    options.AddPolicy("Nueva Politica", app =>
    {
        app.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Nueva Politica");

app.UseAuthorization();

app.MapControllers();

app.Run();
