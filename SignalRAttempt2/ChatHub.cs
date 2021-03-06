﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRAttempt2
{
    public class ChatHub: Hub
    {
        //Call the broadcastMessage method to update clients.
        public void Send(string name, string message, string groupName)
        {
            //Send a Message to all clients;
            if (groupName == "main") Clients.All.InvokeAsync("broadcastMessage", name, message, Context.ConnectionId, "main");
            else Clients.Group(groupName).InvokeAsync("broadcastMessage", name, message, Context.ConnectionId, groupName);
        }

        /// <summary>
        /// This call will put the calling user into a group with the connectionID passed in the "connection" parameter then call groupJoined on the users in that group
        /// which will trigger setup of all the panels and chat shit on the frint end.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task JoinGroup(string connection, string userToChat)
        {
            var group_name = GroupName();
            await Groups.AddAsync(Context.ConnectionId, group_name);          
            await Groups.AddAsync(connection, group_name);
            await Clients.Group(group_name).InvokeAsync("groupjoined",group_name, userToChat);
        }

        private string GroupName()
        {
            var randy = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 9)
                .Select(x => pool[randy.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }
    }
}
