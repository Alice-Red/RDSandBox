using System;
using System.IO;
using System.Threading;
using RUtil.Tcp;

namespace RDSandboxCUI
{
    public class ArcHttpPrac
    {
        private Server server;
        private const string targetPath = "";

        public ArcHttpPrac() {
            server = new Server(12345);
            server.MessageReceived += Server_MessageReceived;
            server.Boot();
        }

        private void Server_MessageReceived(Server sender, MessageReceivedArgs e) {
            Console.WriteLine(e.IpAddress);
            Console.WriteLine(e.Message);
            HttpRequestObject req = new HttpRequestObject(e.Message);

            HttpResponseObject res = new HttpResponseObject(req.HttpVersion);

            if (File.Exists(targetPath + req.Path)) {
                res.ResponseCode = 200;
                
                
                
                
                using (StreamReader sr = new StreamReader(targetPath + req.Path)) {
                    res.Ingredients = sr.ReadToEnd();
                }
            }


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