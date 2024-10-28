using EventPlanning.Data;
using Microsoft.AspNetCore.Mvc;
using Weatheforecast.Entities;

namespace Aspnetcoretestapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly EventPlanningDbContext _dbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, EventPlanningDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

            dbContext.Events.Add(new Event 
            {
                EventId = 1,
                Title = "Test Title",
                Theme = new Theme 
                {
                    ThemeId = 1,
                    ThemeName = "Default",
                },
                Date = DateTime.Now                
            });

            dbContext.SaveChanges();
        }

        [HttpGet]
        public IEnumerable<Event> Index()
        {
            return _dbContext.Events.ToArray();

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
