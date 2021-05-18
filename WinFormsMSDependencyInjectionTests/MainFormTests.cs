using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WinFormsMSDependencyInjection.Tests
{
    [TestClass()]
    public class MainFormTests
    {
        [TestMethod()]
        public void MainFormTest()
        {
            var thingMoq = new Mock<ILogger<ThingTwo>>();
            var relayMoq = new Mock<IOptions<RelayConfig>>();
            relayMoq.Setup(s => s.Value).Returns(new RelayConfig { Position = "Test", Type = "MyType" });

            var thingTwo = new ThingTwo(thingMoq.Object, relayMoq.Object);
            Assert.IsNotNull(thingTwo.Hello());
        }
    }
}