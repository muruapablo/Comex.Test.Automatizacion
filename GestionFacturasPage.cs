using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Comex.Test.Automatizacion.Pages
{
    public class GestionFacturasPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public GestionFacturasPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Elementos
        private IWebElement PestañaGestionFacturas =>
            _wait.Until(d => d.FindElement(By.XPath("/html/body/form/div[3]/div/ul/li[2]/a")));

        private IWebElement BotonCrearFactura =>
            _wait.Until(d => d.FindElement(By.Id("CphContenido_lnkCrearFactura")));

        // Acción: ir a la pestaña de Gestión de Facturas
        public void IrAGestionFacturas()
        {
            Console.WriteLine("Seleccionando pestaña Gestión Facturas Importación...");
            _wait.Until(d => PestañaGestionFacturas.Displayed && PestañaGestionFacturas.Enabled);

            // Ejecutar JS en caso de que sea necesario mostrar la pestaña
            ((IJavaScriptExecutor)_driver).ExecuteScript("tab1();");

            PestañaGestionFacturas.Click();
        }

        // Acción: crear factura
        public void ClickCrearFactura()
        {
            Console.WriteLine("Esperando Botón Crear Factura...");
            _wait.Until(d => BotonCrearFactura.Displayed && BotonCrearFactura.Enabled);
            BotonCrearFactura.Click();
        }
    }
}
