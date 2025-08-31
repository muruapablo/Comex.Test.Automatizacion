using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

public class PanelGestionCarpetaImportacionPage
{
    private readonly IWebDriver _driver; private readonly WebDriverWait _wait;
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
private IWebElement BotonCargaManual => _driver.FindElement(By.Id("CphContenido_lnkCargaManual"));

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

        // Esperar posible recarga tras proveedor (para dropdowns dinámicos)
        Thread.Sleep(1000); // Temporal, ajustar si necesario

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
        _wait.Until(d => new SelectElement(DdlTipoMaterial).Options.Count > 0);
        try
        {
            // Depuración: Mostrar opciones disponibles
            var selectTipoMaterial = new SelectElement(DdlTipoMaterial);
            var opcionesTipoMaterial = selectTipoMaterial.Options.Select(o => $"value='{o.GetAttribute("value")}', text='{o.Text}'").ToList();
            Console.WriteLine($"Opciones disponibles en ddlTipoMaterial: {string.Join(" | ", opcionesTipoMaterial)}");

            // Intentar seleccionar el valor proporcionado
            if (selectTipoMaterial.Options.Any(o => o.GetAttribute("value") == tipoMaterial))
            {
                selectTipoMaterial.SelectByValue(tipoMaterial);
                Console.WriteLine($"Seleccionado tipoMaterial: {tipoMaterial}");
            }
            else if (selectTipoMaterial.Options.Any())
            {
                string primeraOpcion = selectTipoMaterial.Options.First().GetAttribute("value");
                Console.WriteLine($"Valor {tipoMaterial} no encontrado en ddlTipoMaterial. Seleccionando primera opción: {primeraOpcion}");
                selectTipoMaterial.SelectByValue(primeraOpcion);
            }
            else
            {
                throw new NoSuchElementException("El dropdown ddlTipoMaterial no tiene opciones disponibles.");
            }
        }
        catch (ElementNotInteractableException)
        {
            Console.WriteLine("Usando JavaScript para seleccionar ddlTipoMaterial...");
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", DdlTipoMaterial, tipoMaterial);
        }

        // C.Vta.Proveedor
        _wait.Until(d => DdlCvtaProveedor.Displayed && DdlCvtaProveedor.Enabled);
        _wait.Until(d => new SelectElement(DdlCvtaProveedor).Options.Count > 0);
        try
        {
            // Depuración: Mostrar opciones disponibles
            var selectCvtaProveedor = new SelectElement(DdlCvtaProveedor);
            var opcionesCvtaProveedor = selectCvtaProveedor.Options.Select(o => $"value='{o.GetAttribute("value")}', text='{o.Text}'").ToList();
            Console.WriteLine($"Opciones disponibles en dllCVtaProveedor: {string.Join(" | ", opcionesCvtaProveedor)}");

            // Intentar seleccionar el valor proporcionado
            if (selectCvtaProveedor.Options.Any(o => o.GetAttribute("value") == cvtaProveedor))
            {
                selectCvtaProveedor.SelectByValue(cvtaProveedor);
                Console.WriteLine($"Seleccionado cvtaProveedor: {cvtaProveedor}");
            }
            else if (selectCvtaProveedor.Options.Any())
            {
                string primeraOpcion = selectCvtaProveedor.Options.First().GetAttribute("value");
                Console.WriteLine($"Valor {cvtaProveedor} no encontrado en dllCVtaProveedor. Seleccionando primera opción: {primeraOpcion}");
                selectCvtaProveedor.SelectByValue(primeraOpcion);
            }
            else
            {
                throw new NoSuchElementException("El dropdown dllCVtaProveedor no tiene opciones disponibles.");
            }
        }
        catch (ElementNotInteractableException)
        {
            // Intentar clic para desplegar (si es un control personalizado como Select2)
            Console.WriteLine("Intentando desplegar dllCVtaProveedor con clic...");
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", DdlCvtaProveedor);
            Thread.Sleep(500); // Esperar que el menú se despliegue
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", DdlCvtaProveedor, cvtaProveedor);
        }

        // Total FOB (Importe FOB)
       /* _wait.Until(d => CampoImporteFOB.Displayed && CampoImporteFOB.Enabled);
        try
        {
            CampoImporteFOB.Clear();
            CampoImporteFOB.SendKeys(importeFOB);
        }
        catch (ElementNotInteractableException)
        {
            Console.WriteLine("Usando JavaScript para llenar txtImporteFOB...");
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = arguments[1];", CampoImporteFOB, importeFOB);
        }*/

        // Clic en "Carga Manual" para habilitar detalle de factura
        Console.WriteLine("Buscando botón Carga Manual...");
        try
        {
            // Verificar si el botón existe
            var boton = _driver.FindElement(By.Id("CphContenido_lnkCargaManual"));
            Console.WriteLine("Botón Carga Manual encontrado. Verificando visibilidad y estado...");

            // Hacer scroll al botón
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'center'});", boton);
            Thread.Sleep(500); // Esperar que el scroll se complete

            // Esperar que el botón esté visible y habilitado
            _wait.Until(d => boton.Displayed && boton.Enabled);
            Console.WriteLine("Botón Carga Manual visible y habilitado. Pulsando...");

            try
            {
                boton.Click();
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Usando JavaScript para pulsar lnkCargaManual...");
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", boton);
            }
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Error: No se encontró el botón CphContenido_lnkCargaManual.");
            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile("error_boton_carga_manual_no_encontrado.png");
            throw;
        }

        // Esperar que el modal/sección de detalle sea visible (asumiendo un modal genérico)
        try
        {
            Console.WriteLine("Esperando sección de detalle de factura...");
            IWebElement seccionDetalle = _wait.Until(d => d.FindElement(By.CssSelector(".modal-dialog, #CphContenido_modalEditarFactura div")));
            _wait.Until(d => seccionDetalle.Displayed);
            Console.WriteLine("Sección de detalle visible.");
        }
        catch (WebDriverTimeoutException)
        {
            Console.WriteLine("No se detectó modal/sección de detalle, asumiendo que no requiere interacción adicional.");
        }

        // Guardar factura
        Console.WriteLine("Guardando factura...");
        try
        {
            // Hacer scroll al botón Guardar
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'center'});", BotonGuardarFactura);
            Thread.Sleep(500); // Esperar que el scroll se complete

            _wait.Until(d => BotonGuardarFactura.Displayed && BotonGuardarFactura.Enabled);
            BotonGuardarFactura.Click();
        }
        catch (ElementNotInteractableException)
        {
            Console.WriteLine("Usando JavaScript para pulsar lnkUpdateFactura...");
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", BotonGuardarFactura);
        }
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
