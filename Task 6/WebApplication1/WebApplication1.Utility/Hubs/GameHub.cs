using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Utility.Hubs
{
    public class GameHub : Hub
    {
        /*public async Task SendBoardUpdate()
        {
            await Clients.All.SendAsync("ReceiveBoardUpdate");
        }*/

        public async Task SendBoardListChangedInfo()
        {
            await Clients.Others.SendAsync("ChangeBoardList");
        }
    }
}
