﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUtil.Tcp
{
    public class MessageReceivedArgs : EventArgs
    {
        public string IpAddress { get; }
        public string Message { get; }

        public MessageReceivedArgs(string ipAddress, string message) {
            IpAddress = ipAddress;
            Message = message;
        }
    }

    public class ConnectionSuccessfullArgs : EventArgs
    {
        public string IpAddress { get; }

        public ConnectionSuccessfullArgs(string ipAddress) {
            IpAddress = ipAddress;
        }
    }

    public class DisConnectedArgs : EventArgs
    {
        public string IpAddress { get; }

        public DisConnectedArgs(string ipAddress) {
            IpAddress = ipAddress;
        }
    }

    public class ConnectedCountChangedArgs : EventArgs
    {
        public int Count { get; }
        public ConnectedCountChangedArgs(int count) {
            Count = count;
        }
    }

    public class ServerAwakedArgs : EventArgs
    {
        public string[] IpAddress { get; }
        public int Port { get; }

        public ServerAwakedArgs(string[] ipAddress, int port) {
            IpAddress = ipAddress;
            Port = port;
        }
    }





}
