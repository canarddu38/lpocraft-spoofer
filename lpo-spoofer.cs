using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace lpocraftspoofer
{
	public class Program
	{
		public static TcpClient CreateTcpClient(string url)
		{
			var webRequest = WebRequest.Create(url);
			webRequest.Proxy = null;

			var webResponse = webRequest.GetResponse();
			var resposeStream = webResponse.GetResponseStream();

			const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

			var rsType = resposeStream.GetType();
			var connectionProperty = rsType.GetProperty("Connection", flags);

			var connection = connectionProperty.GetValue(resposeStream, null);
			var connectionType = connection.GetType();
			var networkStreamProperty = connectionType.GetProperty("NetworkStream", flags);

			var networkStream = networkStreamProperty.GetValue(connection, null);
			var nsType = networkStream.GetType();
			var socketProperty = nsType.GetProperty("Socket", flags);
			var socket = (Socket)socketProperty.GetValue(networkStream, null);

			return new TcpClient { Client = socket };
		}
		public static void Main()
		{
			Console.Write("Entrez l'ip du serveur: ");
			string ip = Console.ReadLine();
			// Console.WriteLine(ip);
			Byte[] bytesReceived = new Byte[256];
			
			
			var ipEndPoint = new IPEndPoint(IPAddress.Any, 25565);
			TcpListener listener = new TcpListener(ipEndPoint);
			// TcpClient client = CreateTcpClient("http://"+ip+":25565");
			
			listener.Start();

			Socket socket = listener.AcceptSocket();
			while(socket.Connected)
			{
				int bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
				// receivEncoding.ASCII.GetString(bytesReceived, 0, bytes);
				Console.WriteLine(bytes.ToString());
			}
		}
	}
}