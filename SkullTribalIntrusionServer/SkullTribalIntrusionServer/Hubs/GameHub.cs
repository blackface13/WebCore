using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkullTribalIntrusionServer.Hubs
{
    public class GameHub : Hub
    {
        // Players list that save the players connection id and group id
        private static Dictionary<string, string> players = new Dictionary<string, string>();

        /// <summary>
        /// When player join in the game
        /// </summary>
        public override Task OnConnectedAsync()
        {
            // Add player to the players list and set the group id to -2
            players.Add(Context.ConnectionId, "-2");

            return base.OnConnectedAsync();
        }

        /// <summary>
        /// When player re-join the game
        /// </summary>
        //public override Task OnReconnected()
        //{
        //    // Add player to the players list and set the group id to -2
        //    if (players.FirstOrDefault(player => player.Key == Context.ConnectionId).Key == null)
        //        players.Add(Context.ConnectionId, "-2");

        //    return base.OnReconnected();
        //}

        /// <summary>
        /// When player disconnected
        /// </summary>
        public override Task OnDisconnectedAsync(Exception stopCalled)
        {
            string groupName = players[Context.ConnectionId];

            // Remove player from the players list and send message to other player to tell him the opponent left
            players.Remove(Context.ConnectionId);
            Clients.Group(groupName).SendCoreAsync("OpponentLeft", null);

            return base.OnDisconnectedAsync(stopCalled);
        }

        /// <summary>
        /// Search for an alone opponent to connect them together
        /// </summary>
        public void SearchOpponent()
        {
            // Set your groupName to -1 and try to find a player that isn't in any group
            players[Context.ConnectionId] = "-1";
            string aloneOpponentId = players.FirstOrDefault(player => (player.Value == "-1" && player.Key != Context.ConnectionId)).Key;

            if (aloneOpponentId != null) // If player found
            {
                // Set groupId to both of players information in players list
                string groupId = Context.ConnectionId + aloneOpponentId;
                players[Context.ConnectionId] = groupId;
                players[aloneOpponentId] = groupId;

                // Add both of players to the group
                Groups.AddToGroupAsync(Context.ConnectionId, groupId);
                Groups.AddToGroupAsync(aloneOpponentId, groupId);

                // Send Success message to these players
                Clients.Client(Context.ConnectionId).SendCoreAsync("JoinToOpponent", new object[] { 1 });
                Clients.Client(aloneOpponentId).SendCoreAsync("JoinToOpponent", new object[] { 2 });
            }
            else // If player not found
            {
                // Send Fail message to the caller player
                Clients.Caller.SendCoreAsync("JoinToOpponent", new object[] { -1 });
            }
        }

        /// <summary>
        /// Get the position information of players and send it to opponent
        /// </summary>
        /// <param name="x">The received x position</param>
        /// <param name="y">The received y position</param>
        public void SendTransformation(float x, float y)
        {
            string groupId = players[Context.ConnectionId];
            Clients.OthersInGroup(groupId).SendCoreAsync("OpponentTransformation", new object[] { x, y });
        }
    }
}
