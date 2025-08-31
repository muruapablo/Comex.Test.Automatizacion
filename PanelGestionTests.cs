using OpenQA.Selenium; using Microsoft.VisualStudio.TestTools.UnitTesting; using OpenQA.Selenium.Chrome; using System; using System.Threading;

[TestClass] public class PanelGestionTests { private IWebDriver _driver;
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
    _driver.Navigate().GoToUrl("http://localhost:62063/PrincipalNuevo.aspx");
    IWebElement suramericanaIcon = _driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/div/div[7]"));
    suramericanaIcon.Click();

    IWebElement campoUsuario = _driver.FindElement(By.Id("TxtLoginUsuario"));
    IWebElement campoContrasena = _driver.FindElement(By.Id("TxtLoginContrasena"));
    IWebElement botonLogin = _driver.FindElement(By.Id("CmdLogin"));

    campoUsuario.SendKeys("SF77332");
    campoContrasena.SendKeys("Jul*Arg$2025");
    botonLogin.Click();

    Thread.Sleep(2000);
    IWebElement tabImportacion = _driver.FindElement(By.Id("CphContenido_idImportacion"));
    tabImportacion.Click();

    Thread.Sleep(2000);
    IWebElement linkPanelGestion = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/div[2]/a[3]"));
    linkPanelGestion.Click();

    Thread.Sleep(3000);
    PanelGestionCarpetaImportacionPage panelPage = new PanelGestionCarpetaImportacionPage(_driver);
    // panelPage.BuscarPorProveedor("codigoProveedorEjemplo");
    // bool resultadosVisibles = panelPage.SeMostraronResultadosEnGrilla();
    // Assert.IsTrue(resultadosVisibles, "Los resultados de la búsqueda no se mostraron.");
}

[TestMethod]
public void CrearFactura_ValidarCamposYVerificarAlReabrir()
{
    // 1. Navegar a la página de inicio y loguearse
    _driver.Navigate().GoToUrl("http://localhost:62063/PrincipalNuevo.aspx");
    IWebElement suramericanaIcon = _driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[2]/div/div[7]"));
    suramericanaIcon.Click();

    IWebElement campoUsuario = _driver.FindElement(By.Id("TxtLoginUsuario"));
    IWebElement campoContrasena = _driver.FindElement(By.Id("TxtLoginContrasena"));
    IWebElement botonLogin = _driver.FindElement(By.Id("CmdLogin"));

    campoUsuario.SendKeys("SF77332");
    campoContrasena.SendKeys("Jul*Arg$2025");
    botonLogin.Click();

    // 2. Navegar al módulo de importación
    Console.WriteLine("Navegando al módulo de importación...");
    Thread.Sleep(2000);
    IWebElement tabImportacion = _driver.FindElement(By.Id("CphContenido_idImportacion"));
    tabImportacion.Click();

    // 3. Seleccionar "Panel De Gestión"
    Console.WriteLine("Seleccionando Panel De Gestión...");
    Thread.Sleep(2000);
    IWebElement linkPanelGestion = _driver.FindElement(By.XPath("/html/body/form/div[3]/div/div/div[2]/div[2]/a[3]"));
    linkPanelGestion.Click();

    // 4. Esperar carga de la página
    Thread.Sleep(3000);

    // 5. Instanciar Page Object
    PanelGestionCarpetaImportacionPage panelPage = new PanelGestionCarpetaImportacionPage(_driver);

    // 6. Seleccionar pestaña "Gestión Facturas Importación"
    Console.WriteLine("Seleccionando pestaña...");
    panelPage.SeleccionarPestañaGestionFacturas();

    // 7. Datos de prueba
    string numeroFactura = $"FACT_AUTO_{DateTime.Now:yyyyMMdd}"; // Ejemplo: FACT_AUTO_20250831
    string codigoProveedor = "1102"; // Ajusta a un proveedor válido
    string moneda = "040"; // Ajusta
    string fechaFactura = ""; // Formato dd/MM/yyyy
    string tipoMaterial = "1"; // Valor de DDL, ajusta según opciones reales
    string cvtaProveedor = "004"; // Valor esperado para validar
    string importeFOB = ""; // Formato con 4 decimales para Total FOB

    // 8. Crear factura
    try
    {
        Console.WriteLine("Creando factura...");
            panelPage.CrearFactura(codigoProveedor, numeroFactura, fechaFactura, moneda, tipoMaterial, cvtaProveedor, importeFOB);
    }
    catch (WebDriverTimeoutException ex)
    {
        ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("error_crear_factura_timeout.png");
        throw new Exception($"Timeout al crear factura: {ex.Message}");
    }

    // 9. Verificar mensaje de éxito
    bool creacionExitosa = panelPage.EsMensajeExitoVisible();
    Assert.IsTrue(creacionExitosa, "La factura no se creó correctamente.");

    // 10. Buscar factura creada
    panelPage.BuscarPorNumeroFactura(numeroFactura);

    // 11. Abrir factura en modo edición
    panelPage.AbrirFacturaParaEdicion();

    // 12. Validar campo C.Vta.Proveedor
    string valorCvta = panelPage.ObtenerValorCvtaProveedor();
    Assert.AreEqual(cvtaProveedor, valorCvta, $"El valor de C.Vta.Proveedor esperado era {cvtaProveedor}, pero se obtuvo {valorCvta}.");
}

[TestCleanup]
public void Teardown()
{
    if (_driver != null)
    {
        try
        {
            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("teardown.png");
        }
        catch
        {
            // Ignorar si no se puede tomar screenshot
        }
        _driver.Quit();
    }
}
}