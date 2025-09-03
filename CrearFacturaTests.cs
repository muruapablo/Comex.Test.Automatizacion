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
            // Si te afecta el certificado interno:
            options.AddArgument("ignore-certificate-errors");

            driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(60));

            // Timeouts razonables
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
        }


        [Test]
        public void CrearFactura_ManualYMasiva()
        {

            driver.Navigate().GoToUrl("http://172.18.42.103/comexweb/Index.aspx");

            Console.WriteLine("Página cargada. Intentando login...");

            var loginPage = new LoginPage(driver);
            loginPage.Login("SF77332", "Jul*Arg$2025");

            // 2. Navegar hasta Panel de Gestión de Carpeta de Importación
            var menu = new MainMenuPage(driver);
            menu.IrAPanelGestionCarpeta();

            // 3. Dentro del panel, ir a Gestión Facturas y crear factura
            var gestionFacturas = new GestionFacturasPage(driver);
            gestionFacturas.IrAGestionFacturas();
            gestionFacturas.ClickCrearFactura();


            // 4. Completar cabecera factura
            var facturaPage = new FacturaPage(driver);
            facturaPage.CompletarDatosFactura(
                "SEP00037A", "1102",  "SURA",
                "040", "FCA", "C51I", "C51I001", ""
            );

            // 5. Carga manual detalle
            facturaPage.IrCargaManual();
            var detallePage = new FacturaDetallePage(driver);
            detallePage.AgregarDetalle( "10000", "0.0154", "0.0154", "8000000-303");

            // 6. Carga masiva
            facturaPage.IrCargaMasiva();
            driver.FindElement(By.Id("CphContenido_fileUpDetalleExcel"))
                  .SendKeys(@"C:\Users\Pedro Hernadez\Downloads\Modelo_cargas_Masivas.xls");
            driver.FindElement(By.Id("CphContenido_lnkGenerarDetalleExcel")).Click();

            // 7. Guardar factura
            facturaPage.Guardar();

            // Validación
            Assert.That(driver.Url, Does.Contain("PanelGestionCarpetaImportacion.aspx"));
        }
    }
}