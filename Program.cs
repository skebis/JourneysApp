using CsvHelper;
using CsvHelper.Configuration;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using solita_assignment.Classes;
using solita_assignment.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<JourneyContext>();

ImportCsvData();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

void ImportCsvData()
{
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
    };

    using (var reader = new StreamReader(Path.Join(Environment.CurrentDirectory, "2021-05.csv")))
    using (var csv = new CsvReader(reader, config))
    {
        var records = csv.GetRecords<JourneyDto>().Select(row => new Journey()
        {
            JourneyId = Guid.NewGuid(),
            CoveredDistance = row.CoveredDistance,
            Departure = row.Departure,
            DepartureStationId = row.DepartureStationId,
            DepartureStationName = row.DepartureStationName,
            Duration = row.Duration,
            Return = row.Return,
            ReturnStationId = row.ReturnStationId,
            ReturnStationName = row.ReturnStationName
        });

        using (var db = new JourneyContext())
        {
            while (true)
            {
                var items = records.Take(1000).ToList();

                if (items.Any() == false) break;

                db.BulkInsert(items);
                db.SaveChanges();
            }
        }
    }
}
