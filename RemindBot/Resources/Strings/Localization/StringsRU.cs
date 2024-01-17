using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBot.Resources.RU
{
    internal static class Strings
    {
        public static string Get(string stringName)
        {
            switch (stringName)
            {
                case "PROFILE_CREATED": return """
                        Здравствуй!
                        Я только что создал твой профиль.
                        Твой уровень: 1
                        Опыт: 0
                        ___________
                        Введи команду /help 
                        Узнаешь как повысить свой уровень
                        """;
                case "HELLO": return "Привет! ";
                default: return "unexpected error 0x000000, contact admin please";
            }
        }
    }
}
