using Hbsis.Library.Data.Context;

namespace Hbsis.Library.Data.Seed
{
    public class DbRunner
    {
        public static void UpdateDatabase()
        {
            var context = new DataContext();
            context.UpdateDatabase();
        }
    }
}