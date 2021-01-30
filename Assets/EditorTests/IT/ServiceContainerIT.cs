using System.Collections;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.DI;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class ServiceContainerIT
    {
        internal interface ILogger
        {
            void Log();
        }

        internal class Logger : ILogger
        {
            private readonly MessageContainer container;
            private readonly IMessageReceiver receiver;

            public Logger(MessageContainer container, IMessageReceiver receiver)
            {
                this.container = container;
                this.receiver = receiver;
            }

            public void Log()
            {
                receiver.ReceiveMessage(container.Message);
            }
        }

        internal class MessageContainer
        {
            public string Message { get; set; }
        }

        public interface IMessageReceiver
        {
            void ReceiveMessage(string message);
        }

        [ExecuteInEditMode]
        internal class ServiceConsumer : MonoBehaviour
        {
            [Service]
            private readonly ILogger logger;

            public bool IsTestFinished { get; private set; }

            private void Start()
            {
                logger.Log();
                IsTestFinished = true;
            }
        }

        [UnityTest]
        public IEnumerator TestServiceInjection()
        {
            const string TEST_MESSAGE = "Test message";
            var services = ServiceContainerFactory.Create();
            services.AddTransient<ILogger, Logger>();
            services.AddSingleton<MessageContainer>(new MessageContainer() { Message = TEST_MESSAGE });
            var mockMessageReceiver = new Mock<IMessageReceiver>();
            services.AddSingleton<IMessageReceiver>(mockMessageReceiver.Object);

            var component = new GameObject().AddComponent<ServiceConsumer>();
            services.InjectServicesTo(component);
            while (!component.IsTestFinished)
            {
                yield return null;
            }

            mockMessageReceiver.Verify(
                m => m.ReceiveMessage(It.Is<string>(message => message == TEST_MESSAGE)),
                Times.Once);
        }
    }
}