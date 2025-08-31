using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class PanelGestionCarpetaImportacionPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    // Constructor que recibe el driver del navegador
    public PanelGestionCarpetaImportacionPage(IWebDriver driver)
    {
        _driver = driver;
        // Espera explícita de hasta 10 segundos
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    // --- 1. Mapeo de Elementos ---
    // Usamos By.Id porque es el selector más rápido y fiable. 
    // Los IDs los sacas del archivo .aspx
    private IWebElement CampoProveedor => _driver.FindElement(By.Id("CphContenido_txtFilCodProveedor"));
    private IWebElement BotonBuscar => _driver.FindElement(By.Id("CphContenido_lnkBuscar"));
    private IWebElement GrillaResultados => _driver.FindElement(By.Id("CphContenido_GrdFacturasProveedor"));

    // --- 2. Acciones del Usuario ---
    public void IrAPagina()
    {
        // Reemplaza esta URL con la dirección local de tu página
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
            // Esperamos hasta que la grilla sea visible después de la búsqueda
            _wait.Until(d => GrillaResultados.Displayed);
            // Verificamos que la grilla tenga filas de datos (tr) además de la cabecera
            return GrillaResultados.FindElements(By.TagName("tr")).Count > 1;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}