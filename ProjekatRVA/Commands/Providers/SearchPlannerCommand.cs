using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class SearchPlannerCommand : ICommand
    {
        private IReceiver receiver;
        private readonly SearchDto searchDto;
        public SearchPlannerCommand(IReceiver receiver, SearchDto searchDto)
        {
            this.receiver = receiver;
            this.searchDto = searchDto;
        }
        public void Execute()
        {
            receiver.SearchPlanners(searchDto.PlannerName);
        }
    }
}
