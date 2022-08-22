using ProjekatRVA.Commands.Interfaces;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Receivers;

namespace ProjekatRVA.Commands.Providers
{
    public class EditUserCommand : ICommand
    {
        private IReceiver receiver;
        private readonly EditUserDto editUserDto;
        public EditUserCommand(IReceiver receiver, EditUserDto editUserDto)
        {
            this.receiver = receiver;
            this.editUserDto = editUserDto;
        }
        public void Execute()
        {
            receiver.EditUser(editUserDto);
        }
    }
}
