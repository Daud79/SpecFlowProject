using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFlowProject.Support
{
    public class Driver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentDriver;
        private bool _isDisposed;

        public Driver()
        {
            _currentDriver = new Lazy<IWebDriver>(CreateWebDriver);
        }

        /// <summary>
        /// The Selenium IWebDriver instance
        /// </summary>
        public IWebDriver Current => _currentDriver.Value;

        /// <summary>
        /// Creates the ChromeDriver 
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateWebDriver()
        {
            // Instantiate driver
            var chromeDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService());

            // Manage configuration 
            chromeDriver.Manage().Window.Maximize();

            return chromeDriver;
        }

        /// <summary>
        /// Disposes the web driver
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentDriver.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }
    }
}
