using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExpBot.Commands
{
    internal abstract class Command
    {
        public abstract string Name { get; }
        public abstract Task Execute(Message message, TelegramBotClient client);
        public bool Contains(string command)
        {
            return command.Contains(Name);
        }
    }
}
