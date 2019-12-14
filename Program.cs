using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace SantaBot
{
    public class Program
    {
        const ulong GeneralId = 516765447975206915;
        const ulong TestId = 545786319050113066;
        DiscordSocketClient client;
        SocketTextChannel generalChannel;
        SocketTextChannel testChannel;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            client.Log += Log;
            client.Ready += Ready;
            await client.LoginAsync(TokenType.Bot, Secret.Token);
            await client.StartAsync();
            await Task.Delay(-1);
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
            await testChannel.SendMessageAsync($"All systems operational (v0.01 - Hello World!).");
        }
    }
}
