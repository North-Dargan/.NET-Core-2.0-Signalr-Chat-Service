using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRAttempt2
{
    public class ChatHub: Hub
    {
        //Call the broadcastMessage method to update clients.
        public void Send(string name, string message)
        {
            //Send a Message to all clients;
            Clients.All.InvokeAsync("broadcastMessage", name, message);


            //Send a message to just one client
            //Clients.Client("connectionID").InvokeAsync("br")

            //this.Groups.   
        }

        public Task JoinGroup(string groupName, string name)
        {
            //Clients.All.InvokeAsync("broadcastMessage", Context.ConnectionId, Context.ConnectionId + " Joined the group!");

            SendGroupMessage(groupName, name, name.ToString() + " joined the group.");

            return Groups.AddAsync(Context.ConnectionId, groupName);          

        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveAsync(Context.ConnectionId, groupName);
        }

        //Call the broadcastMessage method to update clients.
        public void SendGroupMessage(string groupName, string name, string message)
        {           

            Clients.Group(groupName).InvokeAsync("broadcastGroupMessage", name, message);
             
        }








    }

   
}
