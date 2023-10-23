using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL
{
    public static class Settings
    {
        public static int TimeoutMinutes { get; } = 5;
        public static int TimeoutSeconds { get; } = 0;
        public static string Password { get; } = "W4ttSt0p2023NoSQL";
        public static string User { get; } = "userdeveloperndb";
        public static string Host { get; } = "wattstop.msxbmb7.mongodb.net";
        public static int Port { get; } = 27017;


    }
}
