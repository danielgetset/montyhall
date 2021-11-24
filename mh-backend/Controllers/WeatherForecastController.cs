using Microsoft.AspNetCore.Mvc;

namespace mh_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Doors = new[]
        {
        "Car", "Goat", "Goat"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get(int simulations, bool doorSwitch)
        {

            WeatherForecast[] terms = new WeatherForecast[simulations];

            for (int i = 0; i < simulations; i++)
            {
                // shuffle doors
                Random rnd = new Random();
                string[] doors = Doors.OrderBy(x => rnd.Next()).ToArray();

                //choosedoor
                int chosenDoor = Random.Shared.Next(1, 3);

                // open one door with goat
                int doorWithGoat = Array.IndexOf(doors, "Goat");

                // if the chosen door is the same as the open door with a goat, then instead, open the next door with a goat
                if (doorWithGoat == chosenDoor)
                {
                    doorWithGoat = Array.IndexOf(doors, "Goat", doorWithGoat + 1);
                }

                // add calculations for switched door
                int switchedDoor = chosenDoor;
                for (int x = 0; x < doors.Length; x++)
                {
                    if (x != chosenDoor && x != doorWithGoat)
                    {
                        switchedDoor = x;
                    }
                }

                // successful or not
                bool success = false;
                if (doorSwitch)
                {
                    if (doors[switchedDoor] == "Car")
                    {
                        success = true;
                    }
                } else {
                    if (doors[chosenDoor] == "Car")
                    {
                        success = true;
                    }
                }

                terms[i] = new WeatherForecast
                {
                    door1 = doors[0],
                    door2 = doors[1],
                    door3 = doors[2],
                    chosenDoor = chosenDoor,
                    openDoor = doorWithGoat,
                    switchedDoor = switchedDoor,
                    success = success
                };
            }

            return terms;

            //return Enumerable.Range(1, simulations).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(1, 3),
            //    Summary = MyRandomArray[Random.Shared.Next(MyRandomArray.Length)]
            //})
            //.ToArray();
        }

        //[HttpGet(Name = "Simulation")]
        //public List<Simulation> Get(int simulations, Boolean doorSwitch)
        //{
        //    List<Simulation> SimulationList = new List<Simulation>();

        //    SimulationList.Add(new Simulation(1, 1, true, true));

        //    return SimulationList;
        //}
    }
}