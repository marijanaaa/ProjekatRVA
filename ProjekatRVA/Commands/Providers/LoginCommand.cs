using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class LoginCommand : ICommand
    {
        private IReceiver receiver;
        private readonly LoginDto loginDto;
        public LoginCommand(IReceiver receiver, LoginDto loginDto)
        {
            this.receiver = receiver;
            this.loginDto = loginDto;
        }

        public void Execute()
        {
            if (receiver.CanLogin(loginDto).Result)
            {
                receiver.Login(loginDto);
            }
        }
    }
}
