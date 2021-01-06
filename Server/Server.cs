using Domen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        Socket listener;
        List<ClientHandler> clients = new List<ClientHandler>();

        public Server()
        {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            listener.Bind(endpoint);

        }

        public void Listen()
        {
            listener.Listen(5);
            bool kraj = false;
            try
            {
                while (!kraj)
                {
                    Socket socket = listener.Accept();
                    ClientHandler handler = new ClientHandler(socket, this);
                    clients.Add(handler);
                    Thread clientThread = new Thread(handler.StartHandler);
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Kraj rada");
                kraj = true;
            }
        }

        public void SendToAllClients(Message message)
        {
            foreach(ClientHandler ch in clients)
            {
                ch.SendMessage(message);
            }
        }

        internal void EndClient(ClientHandler handler)
        {
            clients.Remove(handler);
        }

        internal void Stop()
        {
            listener.Close();
            foreach (ClientHandler c in clients)
            {
                c.Stop();
            }
            clients.Clear();
        }
    }
}
