using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using System.Linq;
using System.Collections.Generic;

namespace Skynet.Modules

{
    public class General : ModuleBase
    {
       [Command("ping")]
       public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("Pong!");
        }

        [Command("info")]
        public async Task Info(SocketGuildUser user = null)
        {
            if (user == null)
            {
                var builder = new EmbedBuilder()
                    .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                    .WithDescription("In this message you can see some information about yourself!")
                    .WithColor(new Color(34, 223, 84))
                    .AddField("User ID", Context.User.Id, true)
                    .AddField("Discriminator", Context.User.Discriminator, true)
                    .AddField("Created at", Context.User.CreatedAt.ToString("MM/dd/yyyy"), true)
                    .AddField("Joined at", (Context.User as SocketGuildUser).JoinedAt.Value.ToString("MM/dd/yyyy"), true)
                    .AddField("Roles", string.Join(" ", (Context.User as SocketGuildUser).Roles.Select(x => x.Mention)))
                    .WithCurrentTimestamp();
                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);

            }
            else
            {
                var builder = new EmbedBuilder()
                    .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                    .WithDescription("In this message you can see some information about yourself!")
                    .WithColor(new Color(34, 223, 84))
                    .AddField("User ID", user.Id, true)
                    .AddField("Discriminator", user.Discriminator, true)
                    .AddField("Created at", user.CreatedAt.ToString("MM/dd/yyyy"), true)
                    .AddField("Joined at", user.JoinedAt.Value.ToString("MM/dd/yyyy"), true)
                    .AddField("Roles", string.Join(" ", user.Roles.Select(x => x.Mention)))
                    .WithCurrentTimestamp();
                var embed = builder.Build();
                await Context.Channel.SendMessageAsync(null, false, embed);
            }

        }

        [Command("purge")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task purge(int amount)
        {
            var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

            var message = await Context.Channel.SendMessageAsync($"{messages.Count()} messages deleted successfuly!");
            await Task.Delay(3000);
            await message.DeleteAsync();
        }

        [Command("server")]
        public async Task Server()
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.Guild.IconUrl)
                .WithDescription("In this message you can find some nice information about the current server.")
                .WithTitle($"{Context.Guild.Name} Information")
                .WithColor(new Color(89, 242, 183))
                .AddField("Created at:", Context.Guild.CreatedAt.ToString("MM/dd/yyyy"), true)
                .AddField("Members:", (Context.Guild as SocketGuild).MemberCount + " members", true);
            // This just wont work
                //.AddField("Online Members:", (Context.Guild as SocketGuild).Users.Where(x => x.Status != UserStatus.Offline).Count() + " members", true);
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}
