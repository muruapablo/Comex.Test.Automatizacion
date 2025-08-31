using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class PanelGestionCarpetaImportacionPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    // Constructor con tiempo de espera de 20 segundos
    public PanelGestionCarpetaImportacionPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
    }

    // --- Mapeo de Elementos ---
    private IWebElement CampoProveedor => _driver.FindElement(By.Id("CphContenido_txtFilCodProveedor"));
    private IWebElement BotonBuscar => _driver.FindElement(By.Id("CphContenido_lnkBuscar"));
    private IWebElement GrillaResultados => _driver.FindElement(By.Id("CphContenido_GrdFacturasProveedor"));
    private IWebElement CampoFiltroNumeroFactura => _driver.FindElement(By.Id("CphContenido_txtFilNroFactura"));
    private IWebElement BotonCrearFactura => _driver.FindElement(By.Id("CphContenido_lnkCrearFactura"));
    private IWebElement ModalEditarFactura => _driver.FindElement(By.Id("CphContenido_modalEditarFactura"));
    private IWebElement CampoProveedorModal => _driver.FindElement(By.Id("CphContenido_txtProveedor"));
    private IWebElement CampoNumeroFactura => _driver.FindElement(By.Id("CphContenido_txtNroFacturaEdit"));
    private IWebElement CampoFechaFactura => _driver.FindElement(By.Id("CphContenido_txtFechaFacturaEdit"));
    private IWebElement DdlTipoMaterial => _driver.FindElement(By.Id("CphContenido_ddlTipoMaterial"));
    private IWebElement DdlCvtaProveedor => _driver.FindElement(By.Id("CphContenido_dllCVtaProveedor"));
    private IWebElement CampoMoneda => _driver.FindElement(By.Id("CphContenido_txtMoneda"));
    private IWebElement BotonGuardarFactura => _driver.FindElement(By.Id("CphContenido_lnkUpdateFactura"));
    private IWebElement MensajeExito => _driver.FindElement(By.Id("CphContenido_divMensaje"));
    private IWebElement IconoEditarEnGrilla => _driver.FindElement(By.XPath("//table[@id='CphContenido_GrdFacturasProveedor']//i[contains(@class, 'fa-pen')]"));
    private IWebElement PestañaGestionFacturas => _driver.FindElement(By.XPath("//*[@id='myTabs']/li[2]/a"));
    private IWebElement CampoImporteFOB => _driver.FindElement(By.Id("CphContenido_txtImporteFOB"));

    // --- Acciones Existentes ---
    public void IrAPagina()
    {
        _driver.Navigate().GoToUrl("http://localhost:62063/PanelGestionCarpetaImportacion.aspx");
    }

    public void BuscarPorProveedor(string codigoProveedor)
    {
        CampoProveedor.Clear();
        CampoProveedor.SendKeys(codigoProveedor);
        BotonBuscar.Click();
    }

    public bool SeMostraronResultadosEnGrilla()
    {
        try
        {
            _wait.Until(d => GrillaResultados.Displayed);
            return GrillaResultados.FindElements(By.TagName("tr")).Count > 1;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }

    // Seleccionar pestaña "Gestión Facturas Importación"
    public void SeleccionarPestañaGestionFacturas()
    {
        Console.WriteLine("Seleccionando pestaña Gestión Facturas Importación...");
        _wait.Until(d => PestañaGestionFacturas.Displayed && PestañaGestionFacturas.Enabled);
        ((IJavaScriptExecutor)_driver).ExecuteScript("tab1();");
        PestañaGestionFacturas.Click();
        _wait.Until(d => BotonCrearFactura.Displayed && BotonCrearFactura.Enabled);
    }

    // --- Acciones para Crear y Validar Factura ---
    public void CrearFactura(string codigoProveedor, string numeroFactura, string fechaFactura, string moneda, string tipoMaterial, string cvtaProveedor, string importeFOB)
    {
        try
        {
            // Click en "Crear Factura"
            Console.WriteLine("Esperando BotonCrearFactura...");
            _wait.Until(d => BotonCrearFactura.Displayed && BotonCrearFactura.Enabled);
            BotonCrearFactura.Click();

            // Esperar modal de creación
            Console.WriteLine("Esperando ModalEditarFactura...");
            _wait.Until(d => ModalEditarFactura.Displayed && ModalEditarFactura.GetAttribute("style").Contains("display: block"));

            // Llenar campos obligatorios, comenzando con Número de Factura
            Console.WriteLine("Llenando campos de factura...");

            // Número de Factura
            _wait.Until(d => CampoNumeroFactura.Displayed && CampoNumeroFactura.Enabled);
            try
            {
                CampoNumeroFactura.Clear();
                CampoNumeroFactura.SendKeys(numeroFactura);
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para llenar txtNroFacturaEdit...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", CampoNumeroFactura, numeroFactura);
            }

            // Proveedor
            _wait.Until(d => CampoProveedorModal.Displayed && CampoProveedorModal.Enabled);
            try
            {
                CampoProveedorModal.Clear();
                CampoProveedorModal.SendKeys(codigoProveedor);
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para llenar txtProveedor...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", CampoProveedorModal, codigoProveedor);
            }

            // Fecha Factura
            _wait.Until(d => CampoFechaFactura.Displayed && CampoFechaFactura.Enabled);
            try
            {
                CampoFechaFactura.Clear();
                CampoFechaFactura.SendKeys(fechaFactura);
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para llenar txtFechaFacturaEdit...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", CampoFechaFactura, fechaFactura);
            }

            // Moneda
            _wait.Until(d => CampoMoneda.Displayed && CampoMoneda.Enabled);
            try
            {
                CampoMoneda.Clear();
                CampoMoneda.SendKeys(moneda);
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para llenar txtMoneda...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", CampoMoneda, moneda);
            }

            // Tipo Material
            _wait.Until(d => DdlTipoMaterial.Displayed && DdlTipoMaterial.Enabled);
            try
            {
                new SelectElement(DdlTipoMaterial).SelectByValue(tipoMaterial);
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para seleccionar ddlTipoMaterial...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", DdlTipoMaterial, tipoMaterial);
            }

            // C.Vta.Proveedor
            _wait.Until(d => DdlCvtaProveedor.Displayed && DdlCvtaProveedor.Enabled);
            try
            {
                new SelectElement(DdlCvtaProveedor).SelectByValue(cvtaProveedor);
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para seleccionar dllCVtaProveedor...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", DdlCvtaProveedor, cvtaProveedor);
            }

            // Total FOB (Importe FOB)
            _wait.Until(d => CampoImporteFOB.Displayed && CampoImporteFOB.Enabled);
            try
            {
                CampoImporteFOB.Clear();
                CampoImporteFOB.SendKeys(importeFOB);
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para llenar txtImporteFOB...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", CampoImporteFOB, importeFOB);
            }

            // Guardar factura
            Console.WriteLine("Guardando factura...");
            _wait.Until(d => BotonGuardarFactura.Displayed && BotonGuardarFactura.Enabled);
            BotonGuardarFactura.Click();
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine($"Timeout en CrearFactura: {ex.Message}");
            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("error_crear_factura_timeout.png");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en CrearFactura: {ex.Message}");
            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("error_crear_factura_general.png");
            throw;
        }
    }

    public bool EsMensajeExitoVisible()
    {
        try
        {
            Console.WriteLine("Verificando mensaje de éxito...");
            _wait.Until(d => MensajeExito.Displayed);
            return MensajeExito.Text.ToLower().Contains("creada") || MensajeExito.Text.ToLower().Contains("éxito");
        }
        catch
        {
            return false;
        }
    }

    public void BuscarPorNumeroFactura(string numeroFactura)
    {
        Console.WriteLine("Buscando factura por número...");
        CampoFiltroNumeroFactura.Clear();
        CampoFiltroNumeroFactura.SendKeys(numeroFactura);
        _wait.Until(d => BotonBuscar.Displayed && BotonBuscar.Enabled);
        BotonBuscar.Click();
        _wait.Until(d => GrillaResultados.Displayed);
    }

    public void AbrirFacturaParaEdicion()
    {
        Console.WriteLine("Abriendo factura para edición...");
        _wait.Until(d => IconoEditarEnGrilla.Displayed && IconoEditarEnGrilla.Enabled);
        IconoEditarEnGrilla.Click();
        _wait.Until(d => ModalEditarFactura.Displayed && ModalEditarFactura.GetAttribute("style").Contains("display: block"));
    }

    public string ObtenerValorCvtaProveedor()
    {
        Console.WriteLine("Validando C.Vta.Proveedor...");
        _wait.Until(d => DdlCvtaProveedor.Displayed);
        return new SelectElement(DdlCvtaProveedor).SelectedOption.GetAttribute("value");
    }
}