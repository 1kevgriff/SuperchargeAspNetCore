using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

 public class SyncHub : Hub
    {
        public async Task StartNotify(){
            await Groups.AddToGroupAsync(Context.ConnectionId, "notify-me");
        }
        public async Task EndNotify() {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "notify-me");
        }
    }
    