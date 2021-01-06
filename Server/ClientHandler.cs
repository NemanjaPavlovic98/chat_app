using Domen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientHandler
    {
        private Socket socket;
        private readonly Server server;
        private NetworkStream stream;
        private BinaryFormatter formatter = new BinaryFormatter();

        public string name;


        public ClientHandler(Socket socket, Server server)
        {
            this.socket = socket;
            this.server = server;
            stream = new NetworkStream(socket);
        }
        private bool end = false;
        internal void StartHandler()
        {
            try
            {
                while (!end)
                {
                    Message request = (Message)formatter.Deserialize(stream);
                    ProcessRequest(request);
                }
            }
            catch (IOException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void ProcessRequest(Message request)
        {
            switch (request.Operation)
            {
                case Operation.Login:
                    name = request.Name;
                    Message responce = new Message { IsSuccesful = true };
                    formatter.Serialize(stream, responce);
                    server.SendToAllClients(new Message { Text = $"Usao je u sobu!", Name = name });
                    break;
                case Operation.SendMessage:
                    request.Name = name;
                    server.SendToAllClients(request);
                    break;
                case Operation.EndCommunication:
                    end = true;
                    server.EndClient(this);
                    server.SendToAllClients(new Message { Text = $"Izasao iz u sobe!", Name = name });
                    break;
            }

        }

        internal void SendMessage(Message message)
        {
            formatter.Serialize(stream, message);
        }

        internal void Stop()
        {
            socket.Close();
        }
    }
}
