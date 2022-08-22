using ProjekatRVA.DataInitialization.Interfaces;

namespace ProjekatRVA.DataInitialization.Providers
{
    public class DataInitialization : IDataInitialization
    {
        private readonly IUserInitialization _userInitialization;

        public DataInitialization(IUserInitialization userInitialization)
        {
            _userInitialization = userInitialization;
        }

        void IDataInitialization.DataInitialization()
        {
            _userInitialization.UserInitialization();
        }
    }
}
