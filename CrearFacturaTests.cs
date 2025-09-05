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
            //  Perfil exclusivo para Selenium
            options.AddArgument(@"--user-data-dir=C:\Users\Pedro Hernadez\AppData\Local\Google\Chrome\User Data\SeleniumProfile");

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
            menu.IrAPanelGestionCarpetaSura();

            var gestionFacturas = new GestionFacturasPage(driver);
            gestionFacturas.IrAGestionFacturas();
            gestionFacturas.ClickCrearFactura();

            // 4. Cabecera factura
            var facturaPage = new FacturaPage(driver);
            facturaPage.CompletarDatosFactura(
                "SEP00038J", "1102", "SURA",
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
            menu.IrAPanelGestionCarpetaSura();

            var gestionFacturas = new GestionFacturasPage(driver);
            gestionFacturas.IrAGestionFacturas();
            gestionFacturas.ClickCrearFactura();

            // 4. Cabecera factura
            var facturaPage = new FacturaPage(driver);
            facturaPage.CompletarDatosFactura(
                "SEP00038M", "1102", "SURA",
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

        [Test]
        public void CrearFactura_FCA_CargaManual()
        {
            driver.Navigate().GoToUrl("http://172.18.42.103/comexweb/Login.FIAT.aspx");

            var loginPage = new LoginPage(driver);
            loginPage.LoginFCA("SF77332", "Jul*Arg$2025");

            var menu = new MainMenuPage(driver);
            menu.IrAPanelGestionCarpetaFCA();
            menu.IrAGestionFacturasFCA();
            var gestionFacturas = new GestionFacturasPage(driver);
            gestionFacturas.ClickCrearFactura();

            var facturaPage = new FacturaPageFCA(driver);
            facturaPage.CompletarDatosFacturaFCA(
                "SEP00099I", "15614", "Fiat",  // Cliente Interno = Fiat
                "040", "FCA", ""//DateTime.Today.ToString("dd/MM/yyyy")
            );

            facturaPage.IrCargaManual();
            var detallePage = new FacturaDetallePage(driver);
            detallePage.AgregarDetalle("20000", "0.0200", "0.0250", "0000000401");
            facturaPage.Guardar();
        }

        [Test]
        public void CrearFactura_FCA_CargaMasiva()
        {
            driver.Navigate().GoToUrl("http://172.18.42.103/comexweb/Login.FIAT.aspx");

            var loginPage = new LoginPage(driver);
            loginPage.LoginFCA("SF77332", "Jul*Arg$2025");

            var menu = new MainMenuPage(driver);
            menu.IrAPanelGestionCarpetaFCA();
            menu.IrAGestionFacturasFCA();
            var gestionFacturas = new GestionFacturasPage(driver);
           
            gestionFacturas.ClickCrearFactura();

            var facturaPage = new FacturaPageFCA(driver);
            facturaPage.CompletarDatosFacturaFCA(
                "SEP00099M2", "15614", "Fiat",
                "040", "FCA", ""//DateTime.Today.ToString("dd/MM/yyyy")
            );

            facturaPage.IrCargaMasiva();
            driver.FindElement(By.Id("CphContenido_fileUpDetalleExcel"))
                  .SendKeys(@"C:\Users\Pedro Hernadez\Downloads\Modelo_cargas_Masivas.xls");
            driver.FindElement(By.Id("CphContenido_lnkGenerarDetalleExcel")).Click();

            facturaPage.Guardar();
        }



        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}