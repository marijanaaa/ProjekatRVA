using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.EventDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class AddEventCommand : ICommand
    {
        private IReceiver receiver;
        private readonly AddEventDto addEventDto;
        public AddEventCommand(IReceiver receiver, AddEventDto addEventDto)
        {
            this.receiver = receiver;
            this.addEventDto = addEventDto;
        }
        public void Execute()
        {
            receiver.AddEvent(addEventDto);
        }
    }
}
