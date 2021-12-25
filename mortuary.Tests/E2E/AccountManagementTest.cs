using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace mortuary.Tests
{
    [TestClass]
    public class AccountManagementTests : IDisposable
    {
        public IWebDriver Driver { get; }

        public AccountManagementTests()
        {
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            Driver.Quit();
        }

        [TestMethod]
        public void NormalUserAutoRegister()
        {
            Driver.Navigate().GoToUrl("http://localhost:7634/es/Account/Login?ReturnUrl=%2F");
            Driver.Manage().Window.Size = new System.Drawing.Size(1936, 1056);
            Driver.FindElement(By.Id("registerLink")).Click();
            Driver.FindElement(By.Id("Email")).Click();
            Driver.FindElement(By.Id("Email")).SendKeys("skollars.software.development@gmail.com");
            Driver.FindElement(By.Id("Password")).Click();
            Driver.FindElement(By.Id("Password")).SendKeys("123456");
            Driver.FindElement(By.CssSelector(".form-group:nth-child(7)")).Click();
            Driver.FindElement(By.Id("ConfirmPassword")).Click();
            Driver.FindElement(By.Id("ConfirmPassword")).SendKeys("123456");
            Driver.FindElement(By.CssSelector(".form-horizontal")).Click();
            Driver.FindElement(By.CssSelector(".btn")).Click();
            Assert.AreEqual("Hola skollars.software.development@gmail.com!", Driver.FindElement(By.LinkText("Hola skollars.software.development@gmail.com!")).Text);
        }
    }
}