namespace DataLayer.Service
{
    public partial class AppService 
    {
        AppContext _context;
        public AppService(AppContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
