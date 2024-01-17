using DataLayer;
using DataLayer.Models;
using Telegram.Bot;

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
                User user = await connection.Context.GetUser(message.From.Id);
                if (user != null)
                {
                    answer = "Hello!" + message.From.Username;
                }
                else
                {
                    await connection.Context.SaveNewUser(new User { 
                        TelegramUserId = message.From.Id,
                        Expierence =0,
                        LastActivity = DateTime.Now,
                        Level = 1,
                        Multiplier =1,
                        Streak = 1
                    });
                    answer = "Your profile has been created! Now start geting exp";
                }
            }

            await client.SendTextMessageAsync(message.From.Id, answer);
        }


    }
}
