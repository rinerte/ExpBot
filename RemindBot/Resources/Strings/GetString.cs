using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Str = DataLayer.Enums.Strings;

namespace ExpBot.Resources
{
    internal static class Strings
    {
        internal static string GetString(Str stringName, string language)
        {
            switch (language)
            {
                case "ru": return RU.Strings.Get(stringName);
                default: return "error 0x000001, contact administrator";
            }
        }
    }
}
