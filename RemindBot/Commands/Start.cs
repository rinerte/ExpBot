using DataLayer;
using DataLayer.Models;
using Telegram.Bot;
using Res = ExpBot.Resources.Strings;

namespace ExpBot.Commands
{
    internal class Start : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Telegram.Bot.Types.Message message, TelegramBotClient client)
        {
            string answer = "";
            using (Connection connection = new Connection())
            {
                User? user = await connection.Context.GetUserAsync(message.From.Id);
                if (user != null)
                {
                    answer = Res.GetString("HELLO", user.Language) + ",\n"+(message.From.Username ?? message.From.FirstName);
                }
                else
                {
                    user = new()
                    {
                        TelegramUserId = message.From.Id,
                        Expierence = 0,
                        LastActivity = DateTime.Now.ToUniversalTime(),
                        Level = 1,
                        Multiplier = 1,
                        Streak = 1,
                        Language = "ru"
                    };
                    await connection.Context.SaveNewUser(user);
                    answer = Res.GetString("PROFILE_CREATED",user.Language);
                }
            }

            await client.SendTextMessageAsync(message.From.Id, answer);
        }


    }
}
