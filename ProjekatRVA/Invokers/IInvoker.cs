using ProjekatRVA.Commands.Interfaces;

namespace ProjekatRVA.Invokers
{
    public interface IInvoker
    {
        void AddAndExecute(ICommand command);
    }
}
