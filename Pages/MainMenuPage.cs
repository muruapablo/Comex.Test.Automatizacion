using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Comex.Tests.Automatizacion.Pages
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
            d => d.FindElement(By.Id("CphContenido_lnk_Importacion"))
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
