using System;
using System.Threading;
using RUtil.Tcp;

namespace RDSandboxCUI
{
    public class ArcHttpPrac
    {
        private Server server;

        public ArcHttpPrac()
        {
            server = new Server(12345);
            server.MessageReceived += Server_MessageReceived;
            server.Boot();
            
            
        }

        private void Server_MessageReceived(Server sender, MessageReceivedArgs e)
        {
            Console.WriteLine(e.IpAddress);
            Console.WriteLine(e.Message);
            
            sender.Send(e.IpAddress, "HTTP/1.1 200 OK");
            sender.Send(e.IpAddress, "Content-Type: text/plain; charset=UTF-8");
            sender.Send(e.IpAddress, "Connection: close");
            sender.Send(e.IpAddress, "");
            sender.Send(e.IpAddress, "Copyright (C) Redkun. 2020");
            sender.Send(e.IpAddress, $"now is {DateTime.Now}");
            sender.Disconnect(e.IpAddress);
        }
        
    }
}