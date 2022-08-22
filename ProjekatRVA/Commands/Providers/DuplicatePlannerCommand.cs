using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class DuplicatePlannerCommand : ICommand
    {
        private IReceiver receiver;
        private readonly DuplicateDto duplicateDto;
        public DuplicatePlannerCommand(IReceiver receiver, DuplicateDto duplicateDto)
        {
            this.receiver = receiver;
            this.duplicateDto = duplicateDto;
        }
        public void Execute()
        {
            receiver.DuplicatePlanner(duplicateDto);
        }
    }
}
