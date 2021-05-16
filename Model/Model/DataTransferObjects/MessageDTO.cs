using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataTransferObjects
{
    public class MessageDTO
    {
        public readonly string Format;
        public readonly byte[] MessageData;
        public readonly DateTime SentAt;
        public readonly string SenderUsername;

        public MessageDTO(string format, byte[] messageData, DateTime sentAt, string senderUsername)
        {
            Format = format;
            MessageData = messageData;
            SentAt = sentAt;
            SenderUsername = senderUsername;
        }
    }
}
