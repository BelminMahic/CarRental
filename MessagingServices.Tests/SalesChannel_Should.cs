using Acme.CarRentalService.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.CarRentalService.MessagingServices.Tests
{
    [TestClass]
    public class SalesChannel_Should
    {
        private SalesChannel _channel;
        private MessageConsumerMock _consumerForMsgA;
        private MessageConsumerMock _consumerForMsgB;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _channel = new SalesChannel();

            _consumerForMsgA = new MessageConsumerMock("MessageA");
            _consumerForMsgB = new MessageConsumerMock("MessageB");

            _channel.Register(_consumerForMsgA);
            _channel.Register(_consumerForMsgB);
        }

        [TestMethod]
        public void Successfully_route_message_to_correct_consumers()
        {
            var msg = new Message("MessageA");

            _channel.Broadcast(msg).Wait();

            Assert.AreEqual(1, _consumerForMsgA.MessagesReceived.Count);
            Assert.AreEqual(0, _consumerForMsgB.MessagesReceived.Count);
        }

        [TestMethod]
        public void Support_multiple_consumers_for_a_msg()
        {
            var msg = new Message("MessageA");
            var consumer = new MessageConsumerMock("MessageA");
            _channel.Register(consumer);

            _channel.Broadcast(msg).Wait();

            Assert.AreEqual(1, _consumerForMsgA.MessagesReceived.Count);
            Assert.AreEqual(1, consumer.MessagesReceived.Count);
            Assert.AreEqual(0, _consumerForMsgB.MessagesReceived.Count);
        }
    }

    internal class MessageConsumerMock : IConsumeMessage
    {
        private readonly string _msgTypeOfInterest;
        public List<Message> MessagesReceived { get; private set; }

        public MessageConsumerMock(string messageTypeOfInterest)
        {
            _msgTypeOfInterest = messageTypeOfInterest;

            MessagesReceived = new List<Message>();
        }

        public bool CanConsume(Message message)
        {
            return message.Type.Equals(_msgTypeOfInterest);
        }

        public Task Consume(Message message)
        {
            MessagesReceived.Add(message);

            return Task.CompletedTask;
        }
    }
}
