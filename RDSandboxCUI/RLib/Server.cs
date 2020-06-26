using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RUtil.Tcp
{
    public class Server
    {
        public delegate void ServerAwakedHandler(Server sender, ServerAwakedArgs e);

        public event ServerAwakedHandler ServerAwaked;

        public delegate void MessageReceivedHandler(Server sender, MessageReceivedArgs e);

        public event MessageReceivedHandler MessageReceived;

        public delegate void ConnectionSuccessfullHandler(Server sender, ConnectionSuccessfullArgs e);

        public event ConnectionSuccessfullHandler ConnectionSuccessfull;

        public delegate void DisConnectedHandler(Server sender, DisConnectedArgs e);

        public event DisConnectedHandler DisConnected;

        public delegate void ConnectedCountChangedHandler(Server sender, ConnectedCountChangedArgs e);

        public event ConnectedCountChangedHandler ConnectedCountChanged;


        private int port;
        public int Port { get { return port; } set { port = value; } }

        private int connectedCount = 0;

        public int ConnectedCount {
            get { return connectedCount; }
            private set {
                if (connectedCount != value)
                    ConnectedCountChanged?.Invoke(this, new ConnectedCountChangedArgs(value));
                connectedCount = value;
            }
        }

        private Dictionary<string, Socket> ConnectingList = new Dictionary<string, Socket>();

        // private int ID = 0;

        private bool forcedTermination = false;

        public Server() { connectedCount = 0; }


        public Server(int port) : base() { Create(port); }


        public void Create(int port) { Port = port; }

        public void Boot() {
            Socket server = new Socket(SocketType.Stream, ProtocolType.IP);
            server.Bind(new IPEndPoint(IPAddress.Any, Port));
            server.Listen(8);

            Task.Factory.StartNew(() => { StartAccept(server); });
        }

        private void StartAccept(Socket server) {
            // ホスト名を取得する
            string hostname = Dns.GetHostName();
            // ホスト名からIPアドレスを取得する


            // IPAddress[] adrList = Dns.GetHostAddresses();
            // ServerAwaked?.Invoke(this, new ServerAwakedArgs(new string[]{ipaddress}, Port));
            // ServerAwaked?.Invoke(this, new ServerAwakedArgs(adrList.Select(s => s.ToString()).ToArray(), Port));
            server.BeginAccept(new AsyncCallback(AcceptCallback), server);
        }

        private void AcceptCallback(IAsyncResult ar) {
            var server = (Socket) ar.AsyncState;
            Socket client;
            // int Id = ID;
            // bool exit = false;
            string ipadd = "";
            try {
                client = server.EndAccept(ar);
                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                // server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger , true);
                server.BeginAccept(new AsyncCallback(AcceptCallback), server);
                ConnectionSuccessfull?.Invoke(this, new ConnectionSuccessfullArgs(client.RemoteEndPoint.ToString()));
                ConnectingList.Add(client.RemoteEndPoint.ToString(), client);
                ConnectedCount++;
            } catch {
                return;
            }

            ipadd = client.RemoteEndPoint.ToString();
            while (true) {
                using (MemoryStream ms = new System.IO.MemoryStream()) {
                    byte[] resBytes = new byte[255];

                    int resSize = 0;
                    try {
                        do {
                            resSize = client.Receive(resBytes);
                            if (resSize == 0)
                                break;
                            ms.Write(resBytes, 0, resSize);
                        } while (resBytes[resSize - 1] != '\n');

                        string resMsg = Encoding.UTF8.GetString(ms.ToArray()).Trim('\r', '\n');
                        if (resMsg != "")
                            MessageReceived?.Invoke(this, new MessageReceivedArgs(ipadd, resMsg));
                    } catch (Exception e) {
                        Console.WriteLine(e);
                        break;
                    }
                }
            }
        }

        [Obsolete]
        public void Command(string cmd) { }

        public void SendAll(string message) {
            foreach (var item in ConnectingList) {
                Send(item.Value, message);
            }
        }

        public void Send(string ip, string message) {
            if (ConnectingList.ContainsKey(ip)) {
                Send(ConnectingList[ip], message);
            }
        }

        private static void Send(Socket target, string message) {
            byte[] sendBytes = Encoding.UTF8.GetBytes(message + '\n');
            target?.Send(sendBytes);
        }

        public void Send(Func<Socket, int, bool> target, string message) {
            byte[] sendBytes = Encoding.UTF8.GetBytes(message + '\n');
            for (int i = 0; i < ConnectingList.Count(); i++) {
                if (target(ConnectingList.ElementAt(i).Value, i))
                    ConnectingList.ElementAt(i).Value.Send(sendBytes);
            }
        }

        public void Disconnect(string ip) {
            if (ConnectingList.ContainsKey(ip)) {
                ConnectingList[ip].Disconnect(true);
                // ConnectingList[ip].Shutdown(SocketShutdown.Both);
                ConnectingList[ip].Close();
                DisConnected?.Invoke(this, new DisConnectedArgs(ip));
                ConnectingList.Remove(ip);
                ConnectedCount -= 1;
            }
        }

        public void Disconnect(Func<Socket, int, bool> target) {
            for (int i = 0; i < ConnectingList.Count(); i++) {
                if (target(ConnectingList.ElementAt(i).Value, i)) {
                    ConnectingList.ElementAt(i).Value.Disconnect(true);
                    // ConnectingList.ElementAt(i).Value.Shutdown(SocketShutdown.Send);
                    // ConnectingList.ElementAt(i).Value.Close();
                }
            }
        }


        public void ShutDown() {
            forcedTermination = true;
            foreach (var item in ConnectingList) {
                item.Value.Shutdown(SocketShutdown.Both);
                item.Value.Close();
            }
        }
    }
}