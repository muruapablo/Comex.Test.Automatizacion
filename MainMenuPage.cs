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

        // Botón Importación
        private IWebElement ImportacionBtn => wait.Until(
            d => d.FindElement(By.Id("CphContenido_idImportacion"))
        );

        // Link Panel de Gestión de Carpeta de Importación
        private IWebElement PanelGestionCarpetaBtn => wait.Until(
            d => d.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/div[2]/a[3]"))
        );

        /// <summary>
        /// Navega desde el menú principal hasta la solapa
        /// "Panel de Gestión de Carpeta de Importación".
        /// </summary>
        public void IrAPanelGestionCarpeta()
        {
            ImportacionBtn.Click();
            PanelGestionCarpetaBtn.Click();
        }
    }
}