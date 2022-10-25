using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tec_Ejemplo.Hubs
{
    public class Glass
    {
        public string ConnectionId { get; set; }
        public int GameNumber { get; set; }
    }

    public class GameHub : Hub
    {
        private static List<Glass> GameClientes = new List<Glass>();
        public override Task OnConnectedAsync()
        {

            if (!GameClientes.Select(client => client.ConnectionId)
                .Contains(Context.ConnectionId))
                GameClientes.Add(new Glass
                {
                    ConnectionId =
                    Context.ConnectionId,
                    GameNumber = 100
                }
                );
            Clients.All.SendAsync("ReceiveData", GameClientes);

            return base.OnConnectedAsync();

        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (GameClientes.Select(Clients => Clients.ConnectionId).Contains(Context.ConnectionId))
                GameClientes
                    .Remove(GameClientes
                    .SingleOrDefault(Clients => Clients.ConnectionId == Context.ConnectionId)
                    );


            return base.OnDisconnectedAsync(exception);
        }

        public void play()
        {
            var currentClient = GameClientes.SingleOrDefault(Clients => Clients.ConnectionId == Context.ConnectionId).GameNumber--;

            if (currentClient <= 0)
            {
                Clients.All.SendAsync("ReceiveGameOver", "Game Over!!");
            }
            Clients.All.SendAsync("ReceiveData", GameClientes);

        }




    }
}
