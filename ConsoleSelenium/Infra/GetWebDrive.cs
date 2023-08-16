using OpenQA.Selenium;
using prmToolkit.Selenium;
using prmToolkit.Selenium.Enum;

namespace ConsoleSelenium.Infra
{
    public static class GetWebDrive
    {
        /// <summary>
        /// Instanciar o web drive selenium
        /// </summary>
        /// <returns>
        /// Return instantiated selenium object
        /// </returns>
        public static IWebDriver WebDrive()
        {
            return WebDriverFactory.CreateWebDriver(Browser.Chrome, "C:\\GoogleDriver\\");
        }
    }
}
