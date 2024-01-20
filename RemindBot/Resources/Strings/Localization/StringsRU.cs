using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Str = DataLayer.Enums.Strings;

namespace ExpBot.Resources.RU
{
    internal static class Strings
    {
        public static string Get(Str stringName)
        {
            switch (stringName)
            {
                case Str.PROFILE_CREATED: return """
                        Здравствуй!
                        Я только что создал твой профиль.
                        Твой уровень: 1
                        Опыт: 0
                        ___________
                        Введи команду /help 
                        Узнаешь как повысить свой уровень
                        """;
                case Str.HELP: return """
                        /rows - добавить запись о написанных строках кода
                        /pages - добавить запись о прочитанных страницах книги
                        /articles - добавить запись о прочитанных статьях
                        /info - получить информацию о профиле
                        """;
                case Str.HELP_ROWS: return """
                        Введите количество написанных строк
                        """;
                case Str.HELP_PAGES: return """
                        Введите количество прочитанных страниц
                        """;
                case Str.HELP_ARTICLES: return """
                        Введите количество прочитанных статей
                        """;
                case Str.HELP_ROWS_COMMENT: return """
                        Введите краткий комментарий о том, что было написано
                        """;
                case Str.HELP_PAGES_COMMENT: return """
                        Введите краткий комментарий о том, что было прочитано
                        """;
                case Str.HELP_ARTICLES_COMMENT: return """
                        Введите краткий комментарий о том, что было прочитано
                        """;
                case Str.ERROR_TOO_MUCH_PAGES: return """
                        За один день невозможно столько прочитать
                        НЕ ВЕРЮ!
                        Вернитесь завтра
                        """;
                case Str.ERROR_TOO_MUCH_ROWS: return """
                        За один день невозможно столько кода написать
                        НЕ ВЕРЮ!
                        Вернитесь завтра
                        """;
                case Str.ERROR_TOO_MUCH_ARTICLES: return """
                        За один день невозможно столько прочитать
                        НЕ ВЕРЮ!
                        Вернитесь завтра
                        """;
                case Str.ERROR_NOT_A_NUMBER: return """
                        Мне кажется, это не совсем число.
                        """;
                case Str.SUCCESSFUL: return """
                        Запись добавлена
                        Заработано опыта:
                        """;
                case Str.LEVEL_UP: return """
                        Ура!
                        Ваш уровень повышен!
                        Теперь у вас уровень: 
                        """;
                default: return "unexpected error 0x000000, contact admin please";
            }
        }
    }
}
