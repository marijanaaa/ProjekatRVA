using ProjekatRVA.Commands.Interfaces;

namespace ProjekatRVA.Invokers
{
    public class Invoker : IInvoker
    {
        public void AddAndExecute(ICommand command)
        {
            command.Execute();
        }
    }
}
