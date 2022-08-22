using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class LogoutCommand : ICommand
    {
        private IReceiver receiver;
        private readonly TokenDto tokenDto;
        public LogoutCommand(IReceiver receiver, TokenDto tokenDto)
        {
            this.receiver = receiver;
            this.tokenDto = tokenDto;
        }
        public void Execute()
        {
            receiver.Logout(tokenDto);
        }
    }
}
