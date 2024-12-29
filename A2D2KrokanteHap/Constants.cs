using SQLite;

namespace A2D2KrokanteHap
{
    public class Constants
    {
        private const string DBFileName = "Week4TestDB";


        public const SQLiteOpenFlags flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;


        public static string DatabasePath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DBFileName);
            }
        }

    }
}

