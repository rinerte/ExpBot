using DataLayer.Service;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class Connection : IDisposable
    {
        AppService _appService;
        public AppService Context { get { return _appService; } }
        public Connection()
        {
            DbContextOptionsBuilder<AppContext> cbuilder = new DbContextOptionsBuilder<AppContext>();
            cbuilder.UseNpgsql(Secrets.Const.ConnectionString);
            AppContext context = new AppContext(cbuilder.Options);
            _appService = new AppService(context);
        }
        public void Dispose()
        {
            _appService.Dispose();
        }
    }
}
