using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TestClient
{
    internal static class Program
    {
        const int PORT_NO = 25565;
        const string SERVER_IP = "127.0.0.1";
        
        private static void Main(string[] args)
        {
            Console.WriteLine("---data to send to the server---");
            string textToSend = DateTime.Now.ToString();

            Console.WriteLine("--- create a TCPClient object at the IP and port no.---");
            TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

            Console.WriteLine("--- send the text ---");
            Console.WriteLine("Sending : " + textToSend);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            Console.WriteLine("--- read back the text ---");
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            client.Close();
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.WriteLine("--- this is the end ---");
            Console.ReadLine();
        }
    }
}