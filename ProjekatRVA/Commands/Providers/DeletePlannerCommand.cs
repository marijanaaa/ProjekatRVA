using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class DeletePlannerCommand : ICommand
    {
        private IReceiver receiver;
        private readonly DeleteDto deleteDto;
        public DeletePlannerCommand(IReceiver receiver, DeleteDto deleteDto)
        {
            this.receiver = receiver;
            this.deleteDto = deleteDto;
        }

        public void Execute()
        {
            receiver.DeletePlanner(deleteDto);
        }
    }
}
