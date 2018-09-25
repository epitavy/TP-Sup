﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Server
    {
        private readonly int _port;
        private readonly Socket _sock;
        private readonly List<Socket> _clients;

        public void Run()
        {
            try
            {
                Init();
                while (true)
                {
                    var client = Accept();
                    new Thread(() => HandleClient(client)) {IsBackground = true}.Start();
                    Console.WriteLine("[" + client.RemoteEndPoint + "] " + "Connected");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _sock.Close();
            }
        }

        public void HandleClient(Socket client)
        {
            try
            {
                while (true)
                {
                    ReceiveMessages(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[" + client.RemoteEndPoint + "] " + e.Message);
                client.Close();
            }
        }

        public Server(int port)
        {
            _port = port;
            _sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<Socket>();
        }

        public void Init()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, _port);
            _sock.Bind(ipep);
            _sock.Listen(10);
            Console.WriteLine("Server Started");
        }

        public Socket Accept()
        {
            Socket client = _sock.Accept();
            _clients.Add(client);
            return client;
        }

        public void ReceiveMessages(Socket client)
        {
            byte[] byteArray = new byte[1024];
            int check = client.Receive(byteArray, SocketFlags.None);
            byte[] littleByteArray = new byte[check];
            for (int i = 0; i < check; i++)
            {
                littleByteArray[i] = byteArray[i];
            }
            
            SendMessage(Encoding.ASCII.GetString(littleByteArray), client);
        }

        public void SendMessage(string message, Socket sender)
        {
            int length = message.Length;
            byte[] byteArray = Encoding.ASCII.GetBytes(message);
            

            foreach (Socket client in _clients)
            {
                if (!client.Equals(sender))
                    client.Send(byteArray, length, SocketFlags.None);
            }
        }
    }
}

