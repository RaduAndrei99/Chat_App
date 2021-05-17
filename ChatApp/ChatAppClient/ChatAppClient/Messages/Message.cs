using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppClient.Messages
{
    class Message
    {
        private bool _seen;
        public bool Seen
        {
            get
            {
                return _seen;
            }
        }
        private int _message;
        public int Msg
        {
            get 
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        private string _from;
        public string From
        {
            get
            {
                return _from;
            }
        }

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
        }


        public Message()
        {
            _seen = false;
        }

    }
}
