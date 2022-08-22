using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class AddNewPlannerCommand : ICommand
    {
        private IReceiver receiver;
        private readonly AddNewPlannerDto addNewPlannerDto;
        public AddNewPlannerCommand(IReceiver receiver, AddNewPlannerDto addNewPlannerDto)
        {
            this.receiver = receiver;
            this.addNewPlannerDto = addNewPlannerDto;
        }

        public void Execute()
        {
            receiver.AddNewPlanner(addNewPlannerDto);
        }
    }
}
