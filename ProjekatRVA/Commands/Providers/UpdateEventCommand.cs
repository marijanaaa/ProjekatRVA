using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.EventDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class UpdateEventCommand : ICommand
    {
        private IReceiver receiver;
        private readonly UpdateEventDto updateEventDto;
        public UpdateEventCommand(IReceiver receiver, UpdateEventDto updateEventDto)
        {
            this.receiver = receiver;
            this.updateEventDto = updateEventDto;
        }

        public void Execute()
        {
            receiver.UpdateEvent(updateEventDto);
        }
    }
}
