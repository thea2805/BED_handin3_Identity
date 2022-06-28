using Microsoft.AspNetCore.SignalR;


namespace BED_handin3_Identity.Hub
{
    public interface IUpdaterHub
    {
        Task UpdatePage(string message);
    }

    public class UpdaterHub : Hub<IUpdaterHub>
    {
        public async Task NotifyUpdatePage(string message)
        {
            await Clients.All.UpdatePage(message);
        }
    }
}
