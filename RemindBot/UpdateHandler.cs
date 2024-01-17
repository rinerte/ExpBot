using ExpBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExpBot
{
    internal class UpdateHandler
    {
        static IReadOnlyList<Command> Commands = null;

        public static async Task Handle(ITelegramBotClient arg1, Update update, CancellationToken cancellationToken, IReadOnlyList<Command> commands, TelegramBotClient client)
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

            // Process actions here


        }
        public static Task HandleErrorAsync(ITelegramBotClient arg1, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.ToString());
            return Task.CompletedTask;
        }
    }
}
