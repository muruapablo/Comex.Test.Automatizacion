using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Comex.Test.Automatizacion.Pages
{
    public class MainMenuPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public MainMenuPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        // --- SURA ---
        // Bot�n Importaci�n
        private IWebElement ImportacionBtn => _wait.Until(d => d.FindElement(By.Id("CphContenido_idImportacion")));

        private IWebElement PanelGestionBtnSura => _driver.FindElement((By.XPath("/html/body/form/div[3]/div/div/div[2]/div[2]/a[3]")));
        private IWebElement Pesta�aGestionFacturasSura => _driver.FindElement(By.Id("CphContenido_lnkGestionFacturasImp"));

      // --- FCA ---
        private IWebElement Pesta�aGestionFacturasFCA => _driver.FindElement(By.XPath("//*[@id='myTabs']/li[2]/a/b"));

        // --- Acciones ---
        public void IrAPanelGestionCarpetaSura()
        {
            ImportacionBtn.Click();
            _wait.Until(d => PanelGestionBtnSura.Displayed && PanelGestionBtnSura.Enabled);
            PanelGestionBtnSura.Click();
        }

        public void IrAPanelGestionCarpetaFCA()
        {
            _driver.Navigate().GoToUrl("http://172.18.42.103/comexweb/PanelGestionCarpetaImportacion.aspx");
        }

        public void IrAGestionFacturasSura()
        {
            _wait.Until(d => Pesta�aGestionFacturasSura.Displayed && Pesta�aGestionFacturasSura.Enabled);
            Pesta�aGestionFacturasSura.Click();
        }
     
        public void IrAGestionFacturasFCA()
        {
            _wait.Until(d => Pesta�aGestionFacturasFCA.Displayed && Pesta�aGestionFacturasFCA.Enabled);
            Pesta�aGestionFacturasFCA.Click();
        }
    }
}