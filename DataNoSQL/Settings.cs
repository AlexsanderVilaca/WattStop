using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL
{
    public static class Settings
    {

        private static string Password { get; } = "W4ttSt0p2023NoSQL";
        public static string ConnectionURI { get; } = "mongodb+srv://userdeveloperndb:"+Password+"@wattstop.msxbmb7.mongodb.net/?retryWrites=true&w=majority";

    }
}
