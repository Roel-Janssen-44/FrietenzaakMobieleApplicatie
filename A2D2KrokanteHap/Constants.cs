﻿using SQLite;

namespace A2D2KrokanteHap
{
    public class Constants
    {

        public const string API_BASE_URL = "https://fakestoreapi.com/products";


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

