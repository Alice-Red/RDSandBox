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
            
            sender.SendAll("HTTP/1.1 200 OK");
            sender.SendAll("Content-Type: text/plain; charset=UTF-8");
            sender.SendAll("Connection: close");
            sender.SendAll("");
            sender.SendAll("Copyright (C) Redkun. 2020");
            sender.Disconnect(e.IpAddress);
        }
        
    }
}