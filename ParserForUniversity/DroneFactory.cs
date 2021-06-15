using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace ParserForUniversity
{
    public static class DroneFactory
    {
        /// <summary>
        /// Формируется экземпляр браузера относительно заданных параметров
        /// </summary>
        /// <returns> Экземпляр браузера </returns>
        public static RemoteWebDriver GetDriver() => new ChromeDriver(ChromeDriverService.CreateDefaultService(),
            new ChromeOptions(), TimeSpan.FromMinutes(3)) as RemoteWebDriver;
    }
}