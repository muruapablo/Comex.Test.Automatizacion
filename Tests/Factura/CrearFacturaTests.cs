using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Comex.Test.Automatizacion.Pages;

namespace Comex.Test.Automatizacion.Tests.Factura
{
    public class CrearFacturaTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void CrearFactura_ManualYMasiva()
        {
            // 1. Navegar a login
            driver.Navigate().GoToUrl("http://172.18.42.103/comexweb/Index.aspx");

            // 2. Login
            var loginPage = new LoginPage(driver);
            loginPage.Login("SF77332", "Jul*Arg$2025");

            // 3. Ir al menú de facturas
            var menu = new MainMenuPage(driver);
            menu.IrAGestionFacturas();

            // 4. Completar cabecera factura
            var facturaPage = new FacturaPage(driver);
            facturaPage.CompletarDatosFactura(
                "SEP00037A", "1102", "01/09/2025", "SURA",
                "010", "080", "C51I", "C51I001"
            );

            // 5. Carga manual detalle
            facturaPage.IrCargaManual();
            var detallePage = new FacturaDetallePage(driver);
            detallePage.AgregarDetalle("8000000-303", "10000", "0.0154", "0.0154");

            // 6. Carga masiva
            facturaPage.IrCargaMasiva();
            driver.FindElement(By.Id("CphContenido_fileUpDetalleExcel"))
                  .SendKeys(@"C:\Users\TuUsuario\Documents\Modelo_cargas_Masivas 2.xls");
            driver.FindElement(By.Id("CphContenido_lnkGenerarDetalleExcel")).Click();

            // 7. Guardar factura
            facturaPage.Guardar();

            // Validación
            Assert.That(driver.Url, Does.Contain("PanelGestionCarpetaImportacion.aspx"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}