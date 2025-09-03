using OpenQA.Selenium;

namespace Comex.Test.Automatizacion.Pages
{
    public class FacturaPage
    {
        private readonly IWebDriver driver;

        public FacturaPage(IWebDriver driver) => this.driver = driver;

        private IWebElement NroFactura => driver.FindElement(By.Id("CphContenido_txtNroFacturaEdit"));
        private IWebElement Proveedor => driver.FindElement(By.Id("CphContenido_txtProveedor"));
        private IWebElement Fecha => driver.FindElement(By.Id("CphContenido_txtFechaFacturaEdit"));
        private IWebElement ClienteInterno => driver.FindElement(By.Id("CphContenido_ddlClienteInterno"));
        private IWebElement Moneda => driver.FindElement(By.Id("CphContenido_txtMoneda"));
        private IWebElement CondicionVenta => driver.FindElement(By.Id("CphContenido_dllCVtaProveedor"));
        private IWebElement ProfitCenter => driver.FindElement(By.Id("CphContenido_ddlProfitCenter"));
        private IWebElement CentroCosto => driver.FindElement(By.Id("CphContenido_ddlCentroCosto"));
        private IWebElement BtnCargaManual => driver.FindElement(By.Id("CphContenido_lnkCargaManual"));
        private IWebElement BtnCargaMasiva => driver.FindElement(By.Id("CphContenido_lnkCargaMasiva"));
        private IWebElement BtnGuardar => driver.FindElement(By.Id("CphContenido_lnkUpdateFactura"));

        public void CompletarDatosFactura(string nroFactura, string proveedor, string fecha, string cliente, string moneda, string condicion, string profit, string centroCosto)
        {
            NroFactura.SendKeys(nroFactura);
            Proveedor.SendKeys(proveedor);
            Fecha.SendKeys(fecha);
            ClienteInterno.SendKeys(cliente);
            Moneda.SendKeys(moneda);
            CondicionVenta.SendKeys(condicion);
            ProfitCenter.SendKeys(profit);
            CentroCosto.SendKeys(centroCosto);
        }

        public void IrCargaManual() => BtnCargaManual.Click();
        public void IrCargaMasiva() => BtnCargaMasiva.Click();
        public void Guardar() => BtnGuardar.Click();
    }
}