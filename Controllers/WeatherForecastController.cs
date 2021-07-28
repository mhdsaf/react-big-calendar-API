using System;
using System.Collections.Generic;
using System.Linq;
using calendar_api.Data;
using calendar_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace calendar_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DataContext data;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext data)
        {
            this.data = data;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            // List<Task> allTasks = new List<Task>();
            // allTasks = data.task_data.ToList();
            // System.Console.WriteLine(allTasks[0].start_date.ToString("MM/dd/yyyy"));
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
