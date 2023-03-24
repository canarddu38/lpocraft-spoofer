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
		const int PORT_NO = 25565;
		const string SERVER_IP = "mc.hypixel.net";
		public static void Main()
		{
			Console.Write("Entrez l'ip du serveur: ");
			string ip = Console.ReadLine();
			// Console.WriteLine(ip);
			Byte[] bytesReceived = new Byte[256];
			
			
			var ipEndPoint = new IPEndPoint(IPAddress.Any, 25565);
			TcpListener listener = new TcpListener(ipEndPoint);
			
			listener.Start();
			
			TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
			NetworkStream nwStream = client.GetStream();
			
			Socket socket = listener.AcceptSocket();
			while(socket.Connected && client.Connected)
			{
				
				// receive from local
				Console.WriteLine("--- receive local bytes ---");
				int bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
				Console.WriteLine(bytes.ToString());
				
				// send to server
				Console.WriteLine("--- send the bytes to server ---");
				Console.WriteLine("Sending : " + textToSend);
				nwStream.Write(bytesToSend, 0, bytesToSend.Length);
				
				
				Console.WriteLine("--- receive server bytes ---");
				byte[] bytesToRead = new byte[client.ReceiveBufferSize];
				int bytes = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
				Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
				
				// send to server

				
				
				
				
				
				
				
				
				byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
				
				

				
			}
		}
	}
}