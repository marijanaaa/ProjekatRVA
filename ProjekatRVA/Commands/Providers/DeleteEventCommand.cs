using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class DeleteEventCommand : ICommand
    {
        private IReceiver receiver;
        private readonly int eventId;
        public DeleteEventCommand(IReceiver receiver, int eventId)
        {
            this.receiver = receiver;
            this.eventId = eventId;
        }

        public void Execute()
        {
            receiver.DeleteEvent(eventId);
        }
    }
}
