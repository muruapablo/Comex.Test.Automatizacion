using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Comex.Test.Automatizacion.Pages;
using System;
using System.Diagnostics;
using System.IO;


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
        public void CrearFactura_ManualYMasiva()
        {
            // 1. Abrir la aplicación
            driver.Navigate().GoToUrl("http://localhost:62063/PrincipalNuevo.aspx");
            Console.WriteLine("Página cargada. Intentando login...");

            // 2. Login
            var loginPage = new LoginPage(driver);
            loginPage.Login("SF77332", "Jul*Arg$2025");

            // 3. Ejecutar AutoIt para el proxy
            EjecutarProxyAuth();

            // 4. Ir a menú → Panel Gestión Carpeta de Importación
            var menu = new MainMenuPage(driver);
            menu.IrAPanelGestionCarpeta();

            // 5. Ir a Gestión Facturas → Crear factura
            var gestionFacturas = new GestionFacturasPage(driver);
            gestionFacturas.IrAGestionFacturas();
            gestionFacturas.ClickCrearFactura();

            // 6. Completar cabecera factura
            var facturaPage = new FacturaPage(driver);
            facturaPage.CompletarDatosFactura(
                "SEP00037A", "1102", "SURA",
                "040", "FCA", "C51I", "C51I001", ""
            );

            // 7. Carga manual detalle
            facturaPage.IrCargaManual();
            var detallePage = new FacturaDetallePage(driver);
            detallePage.AgregarDetalle("10000", "0.0154", "0.0154", "8000000-303");

            // 8. Carga masiva
            facturaPage.IrCargaMasiva();
            driver.FindElement(By.Id("CphContenido_fileUpDetalleExcel"))
                  .SendKeys(@"C:\Users\Pedro Hernadez\Downloads\Modelo_cargas_Masivas.xls");
            driver.FindElement(By.Id("CphContenido_lnkGenerarDetalleExcel")).Click();

            // 9. Guardar factura
            facturaPage.Guardar();

            // 10. Validación
            Assert.That(driver.Url, Does.Contain("PanelGestionCarpetaImportacion.aspx"));
        }

        private void EjecutarProxyAuth()
        {
            string autoItScript = @"C:\Users\Pedro Hernadez\Documents\GitHub\COMEXWEB\Comex.Tests.Automatizacion\Documents\ChromeProxyAuth.exe";

            if (File.Exists(autoItScript))
            {
                Console.WriteLine("Ejecutando AutoIt para proxy...");
                Process.Start(autoItScript);
            }
            else
            {
                Console.WriteLine(" No se encontró el ejecutable AutoIt en: " + autoItScript);
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
        }
    }
}