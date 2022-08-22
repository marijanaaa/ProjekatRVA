using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class RegisterCommand : ICommand
    {
        private IReceiver receiver;
        private readonly RegisterDto registerDto;
        public RegisterCommand(IReceiver receiver, RegisterDto registerDto)
        {
            this.receiver = receiver;
            this.registerDto = registerDto;
        }
        public void Execute()
        {
            receiver.RegisterUser(registerDto);
        }
    }
}
