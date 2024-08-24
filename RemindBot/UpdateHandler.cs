using DataLayer;
using DataLayer.Models;
using ExpBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = DataLayer.Models.User;
using Res = ExpBot.Resources.Strings;
using Str = DataLayer.Enums.Strings;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ExpBot
{
    internal class UpdateHandler
    {
        static IReadOnlyList<Commands.Command> Commands = null;

        public static async Task Handle(ITelegramBotClient arg1, Update update, CancellationToken cancellationToken, IReadOnlyList<Commands.Command> commands, TelegramBotClient client)
        {
            Commands = commands;

            switch (update.Type)
            {

                case UpdateType.Message:
                    try
                    {
                        await HandleMessage(update, client);
                    }
                    catch (Exception e)
                    {
                        await HandleErrorAsync(client, e, cancellationToken);
                    }
                    break;
            }
        }

        private static async Task HandleMessage(Update update, TelegramBotClient client)
        {
            Message message = update.Message;
            switch (message.Type)
            {
                case MessageType.Text:
                    await ManageText(update, client);
                    break;
            }
        }

        private async static Task AddExpPoints(TelegramBotClient client, DataLayer.Connection connection, User user, long exp)
        {
            bool lvlUp = false;
            if (user.Level <= Leveling.MAX_LEVEL)
            {
                long setExp = user.Expierence + exp;
                while (setExp >= Leveling.ExpToNextLevel(user.Level) && user.Level <= Leveling.MAX_LEVEL)
                {
                    setExp = setExp - Leveling.ExpToNextLevel(user.Level);
                    await connection.Context.SetLevelAsync(user.TelegramUserId, user.Level + 1);
                    await connection.Context.SetExpierenceAsync(user.TelegramUserId, 0);
                    user.Level += 1;
                    lvlUp = true;
                }
                if (lvlUp)
                {
                    await client.SendTextMessageAsync(user.TelegramUserId, Res.GetString(Str.LEVEL_UP, user.Language) + user.Level);
                }
                
                await connection.Context.SetExpierenceAsync(user.TelegramUserId, setExp);
            }
        }
        private async static Task ManageText(Update update, TelegramBotClient client)
        {
            Message message = update.Message;

            var commands = Commands;

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    try
                    {
                        await command.Execute(message, client);
                        return;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            using (DataLayer.Connection connection = new())
            {
                User? user = await connection.Context.GetUserAsync(message.From.Id);
                string answer = "";

                switch (user.Action)
                {
                    case DataLayer.Enums.Actions.Idle:
                        answer = Res.GetString(Str.HELP, user.Language);
                        break;
                    case DataLayer.Enums.Actions.AddRows:
                        if (user.CurrentData > 0)
                        {
                            await connection.Context.RefreshMultiplierAsync(user.TelegramUserId);
                            long exp = (long)user.CurrentData * Leveling.CodeRowValue(user.Level); 

                            Log log = new()
                            {
                                ActivityName = "/rows",
                                ExpGained = exp,
                                Note = message.Text,
                                Time = DateTime.Now.ToUniversalTime(),
                            };

                            await AddExpPoints(client, connection, user, exp);

                            await connection.Context.AddLogAsync(log);
                            await connection.Context.AddRowsAsync(user.TelegramUserId, user.CurrentData);
                            await connection.Context.ChangeUsersCurrentDataAsync(user.TelegramUserId, 0);
                            await connection.Context.ChangeUsersActionAsync(user.TelegramUserId, DataLayer.Enums.Actions.Idle);
                            answer = Res.GetString(Str.SUCCESSFUL, user.Language) + log.ExpGained;
                        }
                        else
                        {
                            bool result = int.TryParse(message.Text, out var number);
                            if (result == true && number>0)
                            {
                                if (Leveling.MAX_ROWS_PER_DAY >= number + user.RowsThisDay)
                                {
                                    await connection.Context.ChangeUsersCurrentDataAsync(user.TelegramUserId, number);
                                    answer = Res.GetString(Str.HELP_ROWS_COMMENT, user.Language);
                                }
                                else
                                {
                                    await connection.Context.ChangeUsersActionAsync(user.TelegramUserId, DataLayer.Enums.Actions.Idle);
                                    answer = Res.GetString(Str.ERROR_TOO_MUCH_ROWS, user.Language);
                                }
                            }
                            else
                            {
                                answer = Res.GetString(Str.ERROR_NOT_A_NUMBER, user.Language);
                            }
                        }
                        break;
                    case DataLayer.Enums.Actions.AddPages:
                        if (user.CurrentData > 0)
                        {
                            long exp = (long)user.CurrentData * Leveling.BookPageValue(user.Level) * (long)await connection.Context.RefreshMultiplierAsync(user.TelegramUserId);

                            Log log = new()
                            {
                                ActivityName = "/pages",
                                ExpGained = exp,
                                Note = message.Text,
                                Time = DateTime.Now.ToUniversalTime(),
                            };

                            await AddExpPoints(client, connection, user, exp);

                            await connection.Context.AddLogAsync(log);
                            await connection.Context.AddPagesAsync(user.TelegramUserId, user.CurrentData);
                            await connection.Context.ChangeUsersCurrentDataAsync(user.TelegramUserId, 0);
                            await connection.Context.ChangeUsersActionAsync(user.TelegramUserId, DataLayer.Enums.Actions.Idle);
                            answer = Res.GetString(Str.SUCCESSFUL, user.Language) + log.ExpGained;
                        }
                        else
                        {
                            bool result = int.TryParse(message.Text, out var number);
                            if (result == true && number > 0)
                            {
                                if (Leveling.MAX_BOOKPAGES_PER_DAY >= number + user.PagesThisDay)
                                {
                                    await connection.Context.ChangeUsersCurrentDataAsync(user.TelegramUserId, number);
                                    answer = Res.GetString(Str.HELP_PAGES_COMMENT, user.Language);
                                }
                                else
                                {
                                    await connection.Context.ChangeUsersActionAsync(user.TelegramUserId, DataLayer.Enums.Actions.Idle);
                                    answer = Res.GetString(Str.ERROR_TOO_MUCH_PAGES, user.Language);
                                }
                            }
                            else
                            {
                                answer = Res.GetString(Str.ERROR_NOT_A_NUMBER, user.Language);
                            }
                        }

                        break;
                    case DataLayer.Enums.Actions.AddArticles:
                        if (user.CurrentData > 0)
                        {
                            long exp = (long)user.CurrentData * Leveling.ArticleValue(user.Level) * (long)await connection.Context.RefreshMultiplierAsync(user.TelegramUserId);

                            Log log = new()
                            {
                                ActivityName = "/article",
                                ExpGained = exp,
                                Note = message.Text,
                                Time = DateTime.Now.ToUniversalTime(),
                            };

                            await AddExpPoints(client, connection, user, exp);

                            await connection.Context.AddLogAsync(log);
                            await connection.Context.AddArticlesAsync(user.TelegramUserId, user.CurrentData);
                            await connection.Context.ChangeUsersCurrentDataAsync(user.TelegramUserId, 0);
                            await connection.Context.ChangeUsersActionAsync(user.TelegramUserId, DataLayer.Enums.Actions.Idle);
                            answer = Res.GetString(Str.SUCCESSFUL, user.Language) + log.ExpGained;
                        }
                        else
                        {
                            bool result = int.TryParse(message.Text, out var number);
                            if (result == true && number > 0)
                            {
                                if (Leveling.MAX_ARTICLES_PER_DAY >= number + user.ArticlesThisDay)
                                {
                                    await connection.Context.ChangeUsersCurrentDataAsync(user.TelegramUserId, number);
                                    answer = Res.GetString(Str.HELP_ARTICLES_COMMENT, user.Language);
                                }
                                else
                                {
                                    await connection.Context.ChangeUsersActionAsync(user.TelegramUserId, DataLayer.Enums.Actions.Idle);
                                    answer = Res.GetString(Str.ERROR_TOO_MUCH_ARTICLES, user.Language);
                                }
                            }
                            else
                            {
                                answer = Res.GetString(Str.ERROR_NOT_A_NUMBER, user.Language);
                            }
                        }
                        break;
                }
                await client.SendTextMessageAsync(user.TelegramUserId, answer);
            }
        }
        public static Task HandleErrorAsync(ITelegramBotClient arg1, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
            return Task.CompletedTask;
        }
    }
}
