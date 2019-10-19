namespace Hbsis.Library.CrossCutting
{
    public static class EnvironmentProperties
    {
        public static int SessionLifeTime = 10;
        public const string DatabaseName = "Library";
        public static string ConnectionString = "Data Source=localhost;Initial Catalog=Library;Persist Security Info=True;User ID=sa;Password=Hbsis@1234";
    }
}