using EventPlanning.Data;
using Microsoft.AspNetCore.Mvc;
using EventPlanning.Data.Entities;

namespace Aspnetcoretestapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<EventsController> _logger;
        private readonly EventPlanningDbContext _dbContext;

        public EventsController(ILogger<EventsController> logger, EventPlanningDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

            dbContext.Events.Add(new Event 
            {
                Title = "Test Title",
                Theme = new Theme 
                {
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
