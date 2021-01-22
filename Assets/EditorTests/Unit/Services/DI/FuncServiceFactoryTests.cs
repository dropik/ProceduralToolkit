using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.EditorTests.Unit.Services.DI
{
    [Category("Unit")]
    public class FuncServiceFactoryTests
    {
        public interface IExampleService { }

        private Mock<System.Func<IExampleService>> mockServiceProvider;
        private FuncServiceFactory<IExampleService> factory;

        [SetUp]
        public void Setup()
        {
            mockServiceProvider = new Mock<System.Func<IExampleService>>();
            factory = new FuncServiceFactory<IExampleService>(mockServiceProvider.Object);
        }

        [Test]
        public void TestFuncUsedToCreateService()
        {
            factory.CreateService();
            mockServiceProvider.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestFuncValueReturned()
        {
            const string MOCK_SERVICE_ID = "mock service";
            Mock<IExampleService> mockExampleService = new Mock<IExampleService>();
            mockExampleService.Setup(m => m.Equals(It.Is<string>(s => s == MOCK_SERVICE_ID))).Returns(true);
            mockServiceProvider.Setup(m => m.Invoke()).Returns(mockExampleService.Object);

            var service = factory.CreateService();

            Assert.That(service.Equals(MOCK_SERVICE_ID));
        }
    }
}