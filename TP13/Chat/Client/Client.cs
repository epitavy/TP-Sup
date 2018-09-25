using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace Client
{
	public class Client
	{
		private readonly Socket _sock;

		public void Run()
		{
			new Thread(() =>
			{
				while (true) ReceiveData();
			}) {IsBackground = true}.Start();

			while (true) SendData();
		}

		public Client(IPAddress address, int port)
		{
			_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_sock.Connect(address, port);
		}

		public void ReceiveData()
		{
			byte[] byteArray = new byte[1024];
			int nbBytes = _sock.Receive(byteArray, SocketFlags.None);

			string message = Encoding.ASCII.GetString(byteArray);
			Console.WriteLine(message.Substring(0, nbBytes));
		}

		public void SendData()
		{
			string dataToSend = Console.ReadLine();
			
			int nbBytes = dataToSend.Length;
			if(nbBytes > 1024) Console.Error.WriteLine("Socket '{0}' - SendData : Cannot send all buffer", this.ToString());
			_sock.Send(Encoding.ASCII.GetBytes(dataToSend), nbBytes, SocketFlags.None);
		}
	}
}