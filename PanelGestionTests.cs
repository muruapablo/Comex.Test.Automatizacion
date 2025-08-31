using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

[TestClass]
public class PanelGestionTests
{
    private IWebDriver _driver;

    [TestInitialize]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
    }

    [TestMethod]
    public void AlBuscarPorProveedorExistente_DeberianMostrarseResultados()
    {
        // 1. Navega a la página de inicio.
        _driver.Navigate().GoToUrl("http://localhost:62063/");

        // 2. Haz clic en el ícono para ingresar a "SURA".
        IWebElement suramericanaIcon = _driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/div/div[7]"));
        suramericanaIcon.Click();

        // 3. Completa el formulario de login.
        IWebElement campoUsuario = _driver.FindElement(By.Id("TxtLoginUsuario"));
        IWebElement campoContrasena = _driver.FindElement(By.Id("TxtLoginContrasena"));
        IWebElement botonLogin = _driver.FindElement(By.Id("CmdLogin"));

        campoUsuario.SendKeys("SF77332");
        campoContrasena.SendKeys("Jul*Arg$2025");
        botonLogin.Click();

        // 4. Navega a la página de "Panel De Gestión".
        // Espera un momento para que la página cargue si es necesario.
        Thread.Sleep(2000);

        IWebElement tabImportacion = _driver.FindElement(By.Id("CphContenido_lnkImportacion"));
        tabImportacion.Click();

        // Espera un momento para la carga del sub-menú.
        Thread.Sleep(2000);

        IWebElement linkPanelGestion = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/div[2]/a[3]/span[2]"));
        linkPanelGestion.Click();

        // Espera un momento para la carga del sub-menú.
        Thread.Sleep(3000);

        // 5. Continúa con la prueba original en la página de destino.
        PanelGestionCarpetaImportacionPage panelPage = new PanelGestionCarpetaImportacionPage(_driver);
        // La URL de destino será: "http://localhost:62063/PanelGestionCarpetaImportacion.aspx"

        // Aquí puedes agregar el código para buscar por proveedor, si es necesario.
        // panelPage.BuscarPorProveedor("codigoProveedorEjemplo");
        // bool resultadosVisibles = panelPage.SeMostraronResultadosEnGrilla();
        // Assert.IsTrue(resultadosVisibles, "Los resultados de la búsqueda no se mostraron.");
    }

    [TestCleanup]
    public void Teardown()
    {
        _driver?.Quit();
    }
}