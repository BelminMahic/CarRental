using System.Threading.Tasks;

namespace Acme.CarRentalService.DAL
{
    public interface IConsumeMessage
    {
        bool CanConsume(Message message);

        Task Consume(Message message);
    }
}
