using MediatR;
using System;

namespace App.WhoIsParking;

internal class Class1Handler : IRequestHandler<Class1, WeatherModel>
{
    public Task<WeatherModel> Handle(Class1 request, CancellationToken token)
    {
        string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        return Task.FromResult(new WeatherModel()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(request.Goo)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
    }
}
