using System;
using System.Linq;
using RUtil.Tcp;

namespace RDSandboxCUI
{
    internal class Program
    {
        public static void Main(string[] args) {
            ArcHttpPrac ah = new ArcHttpPrac();

            // HttpPrac hp = new HttpPrac();

            Console.ReadKey(true);
        }

        public static void Practice() {
            Server s = new Server(12345);
            s.ServerAwaked += Server_ServerAwaked;
            s.MessageReceived += Server_MessageReceived;
            s.Boot();

            // Client c = new Client("10.1.11.71", 12345);
            //c.Boot();
            while (true) {
                Console.Write("Write here :");
                s.SendAll(Console.ReadLine());
                //c.Send(Console.ReadLine());
                //c.Send(string.Join("", Enumerable.Repeat( "", 900)));
                //c.Send("qaswdefrtgyhujikolpaqwsedrftgyhujikolpqawsedrftgyhuijokplqawsedrftgyhujikolpqawsedrftgyhujikolpaqwsedrftgyhujikolpaqwsedrftgyhuijkoplaqwsedrftgyhuijkolpqawsderftgyhujikolpaqwsedrftgyhujikolpaqwsedrftgyhuijkolpaqwsedrftgyhujikolpaqwsedrftgyhuijkolpaqwsedrftgyhujikolpaqwsedrftgyhuijkolpqawsedftgyhijkolpaqwsedrftgyhuijkolpaqwsedrftgyhuijkolp");
            }
        }


        private static void Server_MessageReceived(Server sender, MessageReceivedArgs e) {
            sender.SendAll(e.Message);
            Console.WriteLine(e.Message);
        }

        private static void Server_ServerAwaked(Server sender, ServerAwakedArgs e) { Console.WriteLine(e.IpAddress); }
    }
}