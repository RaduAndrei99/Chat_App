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
        public readonly List<byte[]> MessageData;
        public readonly bool Seen;
        public readonly DateTime SentAt;
        public readonly DateTime SeenAt;
        public readonly string SenderUsername;

        public MessageDTO(string format, List<byte[]> messageData, bool seen, DateTime sentAt, DateTime seenAt, string senderUsername)
        {
            Format = format;
            MessageData = messageData;
            Seen = seen;
            SentAt = sentAt;
            SeenAt = seenAt;
            SenderUsername = senderUsername;
        }
    }
}
