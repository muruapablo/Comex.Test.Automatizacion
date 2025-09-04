using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Comex.Test.Automatizacion.Pages;
using System;


namespace Comex.Tests.Automatizacion
{
    [TestFixture]
    public class CrearFacturaTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var service = ChromeDriverService.CreateDefaultService();
            var options = new ChromeOptions();



            options.AddArgument("start-maximized");
            options.AddArgument("ignore-certificate-errors");

            driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(60));
        }

        [Test]
        public void CrearFactura_CargaManual()
        {
            // 1. Navegar y login
            driver.Navigate().GoToUrl("http://localhost:62063/");
            var loginPage = new LoginPage(driver);
            loginPage.Login("SF77332", "Jul*Arg$2025");

          

            // 3. Panel de gestión
            var menu = new MainMenuPage(driver);
            menu.IrAPanelGestionCarpeta();

            var gestionFacturas = new GestionFacturasPage(driver);
            gestionFacturas.IrAGestionFacturas();
            gestionFacturas.ClickCrearFactura();

            // 4. Cabecera factura
            var facturaPage = new FacturaPage(driver);
            facturaPage.CompletarDatosFactura(
                "SEP00038H", "1102", "SURA",
                "040", "FCA", "CO", "C5", ""
            );

            // 5. Carga manual
            facturaPage.IrCargaManual();
            var detallePage = new FacturaDetallePage(driver);
            detallePage.AgregarDetalle("10000", "0.0154", "0.0154", "8000000-303");

            // 6. Guardar
            facturaPage.Guardar();

            Assert.That(driver.Url, Does.Contain("PanelGestionCarpetaImportacion.aspx"));
        }

        [Test]
        public void CrearFactura_CargaMasiva()
        {
            // 1. Navegar y login
            driver.Navigate().GoToUrl("http://localhost:62063/");
            var loginPage = new LoginPage(driver);
            loginPage.Login("SF77332", "Jul*Arg$2025");


            // 3. Panel de gestión
            var menu = new MainMenuPage(driver);
            menu.IrAPanelGestionCarpeta();

            var gestionFacturas = new GestionFacturasPage(driver);
            gestionFacturas.IrAGestionFacturas();
            gestionFacturas.ClickCrearFactura();

            // 4. Cabecera factura
            var facturaPage = new FacturaPage(driver);
            facturaPage.CompletarDatosFactura(
                "SEP00038B", "1102", "SURA",
                "040", "FCA", "CO", "C5", ""
            );

            // 5. Carga masiva
            facturaPage.IrCargaMasiva();
            driver.FindElement(By.Id("CphContenido_fileUpDetalleExcel"))
                  .SendKeys(@"C:\Users\Pedro Hernadez\Downloads\Modelo_cargas_Masivas.xls");
            driver.FindElement(By.Id("CphContenido_lnkGenerarDetalleExcel")).Click();

            // 6. Guardar
            facturaPage.Guardar();

            Assert.That(driver.Url, Does.Contain("PanelGestionCarpetaImportacion.aspx"));
        }


        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}