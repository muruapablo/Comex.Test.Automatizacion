using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Comex.Test.Automatizacion.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        }

        private IWebElement SuraIcon => wait.Until(d => d.FindElement(By.CssSelector("div:nth-of-type(7) img")));
        private IWebElement Usuario => wait.Until(d => d.FindElement(By.Id("TxtLoginUsuario")));
        private IWebElement Contrasena => wait.Until(d => d.FindElement(By.Id("TxtLoginContrasena")));

        private IWebElement UsuarioFCA => wait.Until(d => d.FindElement(By.Id("TxtLoginUsuario")));
        private IWebElement ContrasenaFCA => wait.Until(d => d.FindElement(By.Id("TxtLoginContrasena")));

        private IWebElement BtnIngresar => wait.Until(d => d.FindElement(By.Id("CmdLogin")));

        public void Login(string user, string pass)
        {
            // Paso 1: entrar a la opción SURA
            SuraIcon.Click();

            // Paso 2: completar login
            Usuario.Clear();
            Usuario.SendKeys(user);

            Contrasena.Clear();
            Contrasena.SendKeys(pass);

            BtnIngresar.Click();
        }

        public void LoginFCA(string user, string pass)
        {
            driver.Navigate().GoToUrl("http://172.18.42.103/comexweb/Login.FIAT.aspx");

            wait.Until(d => Usuario.Displayed && Usuario.Enabled);
            UsuarioFCA.Clear();
            UsuarioFCA.SendKeys(user);

            ContrasenaFCA.Clear();
            ContrasenaFCA.SendKeys(pass);
            BtnIngresar.Click();
        }



    }
}