using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.CarRentalService.DAL
{
    public abstract class MessageChannel
    {
        private List<IConsumeMessage> _consumers;
        public MessageChannel()
        {
            _consumers = new List<IConsumeMessage>();
        }

        public virtual Task Register(IConsumeMessage messageConsumer)
        {
            _consumers.Add(messageConsumer);

            return Task.CompletedTask;
        }

        public async virtual Task Broadcast(Message message)
        {
            foreach (var consumer in _consumers)
            {
                if (consumer.CanConsume(message))
                {
                    await consumer.Consume(message);
                }
            }
        }
    }
}
