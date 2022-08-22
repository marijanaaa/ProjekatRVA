using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class UpdatePlannerCommand : ICommand
    {
        private IReceiver receiver;
        private readonly EditPlannerDto editPlannerDto;
        public UpdatePlannerCommand(IReceiver receiver, EditPlannerDto editPlannerDto)
        {
            this.receiver = receiver;
            this.editPlannerDto = editPlannerDto;
        }

        public void Execute()
        {
            receiver.UpdatePlanner(editPlannerDto);
        }
    }
}
