using DataLayer.Models;

namespace DataLayer.Interface
{
    public partial interface IAppService
    {
        public Task<User?> GetUser(long userId);
    }
}
