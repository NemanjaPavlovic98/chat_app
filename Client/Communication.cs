using Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = Domen.Message;

namespace Client
{
    public class Communication
    {
        private Socket socket;
        private NetworkStream stream;
        private BinaryFormatter formatter = new BinaryFormatter();

        //singleton pattern
        private Communication()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        private static Communication instance;
        public static Communication Instance
        {
            get
            {
                if (instance == null) instance = new Communication();
                return instance;
            }
        }

        public void Connect()
        {
            socket.Connect("127.0.0.1", 9999);
            stream = new NetworkStream(socket);
        }

        internal bool Login(string text)
        {
            formatter.Serialize(stream, new Message { Operation = Operation.Login, Name = text });
            Message response = (Message)formatter.Deserialize(stream);
            return response.IsSuccesful;
        }

        internal void SendMessage(string text)
        {
            try
            {
                formatter.Serialize(stream, new Message { Text = text, Operation = Operation.SendMessage });
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public void ListenMessages(TextBox textBox)
        {
            Thread listener = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        Message message = (Message)formatter.Deserialize(stream);
                        textBox.Invoke(
                            new Action(
                                () => textBox.Text += DateTime.Now.ToString("HH:mm")+ $"  {message.Name}: {message.Text}"  + "\r\n" 
                                ));
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message); 
                }

            });
            listener.Start();
        }

        internal void EndCommunication()
        {
            formatter.Serialize(stream, new Message { Operation = Operation.EndCommunication });
        }
       
    }
}

