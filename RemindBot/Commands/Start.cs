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
            using (Connection connection = new Connection())
            {
                User user = await connection.Context.GetUser(message.From.Id);
                if (user != null)
                {
                    await client.SendTextMessageAsync(message.From.Id, "Hello!" + message.From.Username);
                }
                else
                {
                    await connection.Context.SaveNewUser(new User { TelegramUserId = message.From.Id });
                    await client.SendTextMessageAsync(message.From.Id, "Your profile has been created! Now start geting exp");
                }
            }
        }


    }
}
