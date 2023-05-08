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

// Currently hard-coded file names
if (!File.Exists(Path.Join(Environment.CurrentDirectory, JourneyContext.dbFileName)))
{
    ImportCsvData<Journey>(new string[]{ "2021-05.csv", "2021-06.csv", "2021-07.csv" });
    ImportCsvData<Station>(new string[]{ "Helsingin_ja_Espoon_kaupunkipyöräasemat_avoin.csv" });
}

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

// Insert all excel data from csv files to database, type T is the class that is getting imported.
void ImportCsvData<T>(string[] csvFiles)
{
    int importRate = 5000;

    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true
    };

    try
    {
        foreach (string csvFile in csvFiles)
        {
            System.Diagnostics.Debug.WriteLine("Started importing {0}");
            // Read files from root directory (where solution-file is)
            using (var reader = new StreamReader(Path.Join(Environment.CurrentDirectory, csvFile)))
            {
                using (var csv = new CsvReader(reader, csvConfig))
                {
                    int totalImports = 0;

                    if (typeof(T) == typeof(Journey))
                    {
                        var records = csv.GetRecords<JourneyDto>()
                            .Where(row => 
                            (row.DepartureStationId > 0) &&
                            (row.ReturnStationId > 0) &&
                            (row.CoveredDistance is not null and > 10.0) &&
                            (row.Duration >= 10) &&
                            (DateTime.Compare(row.Return, row.Departure) > 0))
                            .Select(row => new Journey()
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
                            db.Database.Migrate();
                            while (true)
                            {
                                var items = records.Take(importRate).ToList();

                                if (items.Any() == false) break;

                                db.BulkInsert(items);
                                db.SaveChanges();
                                totalImports += items.Count;
                                System.Diagnostics.Debug.WriteLine(totalImports);
                            }
                        }
                    }
                    else if (typeof(T) == typeof(Station))
                    {
                        var records = csv.GetRecords<StationDto>().Select(row => new Station()
                        {
                            StationId = Guid.NewGuid(),
                            IdInt = row.IdInt,
                            NameFinnish = row.NameFinnish,
                            NameSwedish = row.NameSwedish,
                            NameEnglish = row.NameEnglish,
                            AddressFinnish = row.AddressFinnish,
                            AddressSwedish = row.AddressSwedish,
                            CityFinnish = row.CityFinnish,
                            CitySwedish = row.CitySwedish,
                            Operator = row.Operator,
                            Capacity = row.Capacity,
                            LocationX = row.LocationX,
                            LocationY = row.LocationY,
                        });
                        using (var db = new JourneyContext())
                        {
                            db.Database.Migrate();
                            while (true)
                            {
                                var items = records.Take(importRate).ToList();

                                if (items.Any() == false) break;

                                db.BulkInsert(items);
                                db.SaveChanges();
                                totalImports += items.Count;
                                System.Diagnostics.Debug.WriteLine(totalImports);
                            }
                        }
                    }
                }
            }
        }
    }
    catch (DirectoryNotFoundException ex)
    {
        Console.WriteLine("Make sure the directory or file path is correct. " + ex.Message);
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine("Make sure the file(s) are named correctly (2021-05.csv, 2021-06.csv, 2021-07.csv or Helsingin_ja_Espoon_kaupunkipyöräasemat_avoin.csv). " + ex.Message);
    }
    catch (IOException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Unhandled exception occurred. " + ex.Message);
    }
}
