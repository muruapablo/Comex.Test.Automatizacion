using OpenQA.Selenium;

namespace Comex.Test.Automatizacion.Pages
{
    public class FacturaDetallePage
    {
        private readonly IWebDriver driver;

        public FacturaDetallePage(IWebDriver driver) => this.driver = driver;

        private IWebElement Cantidad => driver.FindElement(By.Id("CphContenido_txtCantidad"));
        private IWebElement ImpUnitFOB => driver.FindElement(By.Id("CphContenido_txtImpUnitFOBDet"));
        private IWebElement ImpUnitFFCA => driver.FindElement(By.Id("CphContenido_txtImpUniFFCADet"));
        private IWebElement Plano => driver.FindElement(By.Id("CphContenido_txtPlano"));
        private IWebElement BtnAgregar => driver.FindElement(By.Id("CphContenido_btnAgregarDetalle"));

        public void AgregarDetalle( string cantidad, string fob, string ffca, string plano)
        {
           
            Cantidad.SendKeys(cantidad);
            ImpUnitFOB.SendKeys(fob);
            ImpUnitFFCA.SendKeys(ffca);
            Plano.SendKeys(plano);
            BtnAgregar.Click();
        }
    }
}
