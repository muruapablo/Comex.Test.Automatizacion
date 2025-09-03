using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Comex.Test.Automatizacion.Pages
{
    public class MainMenuPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public MainMenuPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Bot�n Importaci�n
        private IWebElement ImportacionBtn => wait.Until(
            d => d.FindElement(By.Id("CphContenido_idImportacion"))
        );

        // Link Panel de Gesti�n de Carpeta de Importaci�n
        private IWebElement PanelGestionCarpetaBtn => wait.Until(
            d => d.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/div[2]/a[3]"))
        );

        /// <summary>
        /// Navega desde el men� principal hasta la solapa
        /// "Panel de Gesti�n de Carpeta de Importaci�n".
        /// </summary>
        public void IrAPanelGestionCarpeta()
        {
            ImportacionBtn.Click();
            PanelGestionCarpetaBtn.Click();
        }
    }
}