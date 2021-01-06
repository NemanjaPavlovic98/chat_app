using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    public enum Operation { Login, SendMessage, EndCommunication}
    [Serializable]
    public class Message
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public Operation Operation { get; set; }
        public bool IsSuccesful { get; set; }


    }
}
