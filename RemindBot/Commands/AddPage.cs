using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

using Res = ExpBot.Resources.Strings;

namespace ExpBot.Commands
{
    internal class AddPage : Command
    {
        public override string Name => "/pages";

        public override async Task Execute(Telegram.Bot.Types.Message message, TelegramBotClient client)
        {
            User user = null;

            using(Connection connection = new Connection())
            {
                user = await connection.Context.GetUserAsync(message.From.Id);
                if (user == null) return;
                await connection.Context.ChangeUsersActionAsync(user.TelegramUserId, DataLayer.Enums.Actions.AddPages);
            }
            await client.SendTextMessageAsync(message.From.Id, Res.GetString(DataLayer.Enums.Strings.HELP_PAGES, user.Language));
        }
    }
}
