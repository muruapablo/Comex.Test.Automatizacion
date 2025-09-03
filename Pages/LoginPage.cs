using OpenQA.Selenium;

namespace Comex.Tests.Automatizacion.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver) => this.driver = driver;

        private IWebElement Usuario => driver.FindElement(By.Id("TxtLoginUsuario"));
        private IWebElement Contrasena => driver.FindElement(By.Id("TxtLoginContrasena"));
        private IWebElement BtnIngresar => driver.FindElement(By.Id("CmdLogin"));

        public void Login(string user, string pass)
        {
            Usuario.SendKeys(user);
            Contrasena.SendKeys(pass);
            BtnIngresar.Click();
        }
    }
}