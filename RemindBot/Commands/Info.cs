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
    internal class GetInfo : Command
    {
        public override string Name => "/info";

        public override async Task Execute(Telegram.Bot.Types.Message message, TelegramBotClient client)
        {
            User user = null;

            using(Connection connection = new Connection())
            {
                user = await connection.Context.GetUserAsync(message.From.Id);
                if (user == null) return;

                string answer = "";
                answer += " Your level:" + user.Level + "\n";
                answer += " Your exp points:" + user.Expierence + "\n";
                answer += "Exp to next level:" + (Leveling.ExpToNextLevel(user.Level) - user.Expierence) + "\n";
                await client.SendTextMessageAsync(message.From.Id, answer);
            }
            
        }
    }
}
