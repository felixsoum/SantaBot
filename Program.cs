using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SantaBot
{
    public class Program
    {
        const ulong GeneralId = 516765447975206915;
        const ulong TestId = 545786319050113066;
        private const long Me = 139024035894788096;
        DiscordSocketClient client;
        SocketTextChannel generalChannel;
        SocketTextChannel testChannel;
        static Random random = new Random();

        List<string> participants = new List<string>()
        {
            "Nanith Omicron",
            "Alonso",
            "Nicolas",
            //"Alejandro",
            "Sarkis",
            "Samuel",
            //"Minh",
            "Youcef",
            "Alexandre",
            "Théophile",
            "Philip",
            "Leo",
            //"Daniel",
            "Julian",
            "Angeline"
        };

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            client.Log += Log;
            client.MessageReceived += MessageReceived;
            client.Ready += Ready;
            await client.LoginAsync(TokenType.Bot, Secret.Token);
            await client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "!name")
            {
                string name = $"Your username is {message.Author.Username}";
                await message.Channel.SendMessageAsync(name);
            }
            else if (message.Content == "!id")
            {
                string name = $"Your id is {message.Author.Id}";
                await message.Channel.SendMessageAsync(name);
            }
            else if (message.Content == "!raffle" && message.Author.Id == Me)
            {
                if (message.Author.Id == Me)
                {
                    string winner = participants[random.Next(participants.Count)];
                    await message.Channel.SendMessageAsync($"The Santa raffle #1 winner is: {winner} :gift: Prize = Cuphead");
                }
                else
                {
                    await message.Channel.SendMessageAsync($"User {message.Author.Username} is not authorized to use !raffle.");
                }
            }
            else if (message.Content == "!who")
            {
                string who = String.Join(", ", participants);
                who = "Participants eligible for the raffle are:\n" + who;
                await message.Channel.SendMessageAsync(who);
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Ready()
        {
            generalChannel = (SocketTextChannel)client.GetChannel(GeneralId);
            testChannel = (SocketTextChannel)client.GetChannel(TestId);
            await testChannel.SendMessageAsync($"All systems operational (v0.07 - Raffle #1).");
        }
    }
}
