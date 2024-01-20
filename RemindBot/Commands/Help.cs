using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Res = ExpBot.Resources.Strings;
using Str = DataLayer.Enums.Strings;

namespace ExpBot.Commands
{
    internal class Help : Command
    {
        public override string Name => "/help";

        public override async Task Execute(Telegram.Bot.Types.Message message, TelegramBotClient client)
        {
            User user = null;

            using (Connection connection = new Connection())
            {
                user = await connection.Context.GetUserAsync(message.From.Id);
            }
            if (user == null) return;
            await client.SendTextMessageAsync(message.From.Id, Res.GetString(Str.HELP, user.Language));
        }
    }
}
