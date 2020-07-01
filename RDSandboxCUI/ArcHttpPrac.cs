using System;
using System.IO;
using System.Threading;
using RUtil.Tcp;

namespace RDSandboxCUI
{
    public class ArcHttpPrac
    {
        private Server server;
        private const string targetPath = "target";

        public ArcHttpPrac() {
            server = new Server(12345);
            server.MessageReceived += Server_MessageReceived;
            server.Boot();
        }

        private void Server_MessageReceived(Server sender, MessageReceivedArgs e) {
            Console.WriteLine(e.IpAddress);
            Console.WriteLine(e.Message);
            HttpRequestObject req = new HttpRequestObject(e.Message);
            
            HttpResponseObject res = new HttpResponseObject("1.1");
            
            string localPath = targetPath + req.Path;
            if (File.Exists(localPath)) {
                res.ResponseCode = 200;
            
                var ext = Path.GetExtension(localPath);
                res.StoreHeader("Content-Type", Extension.ToContentType[ext.Trim('.')] + ";");
                res.StoreHeader("Connection", "keep-alive");
            
                using (FileStream fs = new FileStream(targetPath + req.Path, FileMode.Open,FileAccess.Read)) {
                    byte[] bs = new byte[fs.Length];
                    fs.Read(bs, 0, bs.Length);
                    res.Ingredients = bs;
                }
            } else {
                res.ResponseCode = 404;
            }
            
            sender.Send(e.IpAddress,res.ToByteArray());


            // sender.Send(e.IpAddress, "HTTP/1.1 200 OK\r\n");
            // sender.Send(e.IpAddress, "Content-Type: text/plain; charset=UTF-8\r\n");
            // sender.Send(e.IpAddress, "Connection: close\r\n");
            // sender.Send(e.IpAddress, "\r\n");
            // sender.Send(e.IpAddress, "Copyright (C) Redkun. 2020\r\n");
            // sender.Send(e.IpAddress, $"now is {DateTime.Now}\r\n");
            //
            // sender.Disconnect(e.IpAddress);
        }
    }
}