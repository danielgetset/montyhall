using Microsoft.VisualStudio.TestTools.UnitTesting;
using mh_backend.Controllers;
using mh_backend;
using Moq;
using Microsoft.Extensions.Logging;

namespace mh_Testnew
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ResultNotNull()
        {
            ILogger<WeatherForecastController> logger = Mock.Of<ILogger<WeatherForecastController>>();
            WeatherForecastController controller = new WeatherForecastController(logger);

            WeatherForecast[] result = (WeatherForecast[])controller.Get(100, true);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CorrectNumberOfSimulations()
        {
            ILogger<WeatherForecastController> logger = Mock.Of<ILogger<WeatherForecastController>>();
            WeatherForecastController controller = new WeatherForecastController(logger);

            WeatherForecast[] result = (WeatherForecast[])controller.Get(100, true);

            Assert.AreEqual(100, result.Length);
        }

        [TestMethod]
        public void ChosenOpenSwitchedAreNotEqual()
        {
            ILogger<WeatherForecastController> logger = Mock.Of<ILogger<WeatherForecastController>>();
            WeatherForecastController controller = new WeatherForecastController(logger);

            WeatherForecast[] result = (WeatherForecast[])controller.Get(100, true);

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreNotEqual(result[i].chosenDoor, result[i].openDoor);
                Assert.AreNotEqual(result[i].chosenDoor, result[i].switchedDoor);
                Assert.AreNotEqual(result[i].switchedDoor, result[i].openDoor);
            }
        }

        [TestMethod]
        public void ChosenDoorSuccess()
        {
            ILogger<WeatherForecastController> logger = Mock.Of<ILogger<WeatherForecastController>>();
            WeatherForecastController controller = new WeatherForecastController(logger);

            WeatherForecast[] result = (WeatherForecast[])controller.Get(100, false);

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].success)
                {
                    if (result[i].chosenDoor == 0)
                    {
                        Assert.AreEqual(result[i].door1, "Car");
                    }
                    else if (result[i].chosenDoor == 1)
                    {
                        Assert.AreEqual(result[i].door2, "Car");
                    }
                    else if (result[i].chosenDoor == 2)
                    {
                        Assert.AreEqual(result[i].door3, "Car");
                    }
                }
                
            }
        }

        [TestMethod]
        public void SwitchedDoorSuccess()
        {
            ILogger<WeatherForecastController> logger = Mock.Of<ILogger<WeatherForecastController>>();
            WeatherForecastController controller = new WeatherForecastController(logger);

            WeatherForecast[] result = (WeatherForecast[])controller.Get(100, true);

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].success)
                {
                    if (result[i].switchedDoor == 0)
                    {
                        Assert.AreEqual(result[i].door1, "Car");
                    }
                    else if (result[i].switchedDoor == 1)
                    {
                        Assert.AreEqual(result[i].door2, "Car");
                    }
                    else if (result[i].switchedDoor == 2)
                    {
                        Assert.AreEqual(result[i].door3, "Car");
                    }
                }

            }
        }

    }
}