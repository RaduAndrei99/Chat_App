/***************************************************************************
 *                                                                         *
 *  Autor:  Cojocaru Constantin-Cosmin                                     *
 *  Grupa:  1309A                                                          *
 *  Fisier: MessageDTO.cs                                                  *
 *                                                                         *
 *  Descriere: Clasa pentru transferul datelor referitoare la mesaj        *
 *                                                                         *
 ***************************************************************************/

using System;

namespace Model.DataTransferObjects
{
    /// <summary>
    /// Clasa ce incapsuleaza datele mesajelor din baza de date
    /// </summary>
    public class MessageDTO
    {
        /// <summary>Formatul mesajului</summary>
        public readonly string Format;

        /// <summary>Datele din mesaj</summary>
        public readonly byte[] MessageData;

        /// <summary>Data de trimitere a mesajului</summary>
        public readonly DateTime SentAt;

        /// <summary>Numele utilizatorului care a trimis mesajul</summary>
        public readonly string SenderUsername;

        /// <summary>Constructorul ce initializeaza un mesaj</summary>
        /// <param name="format">Formatul mesajului</param>
        /// <param name="messageData">Datele din mesaj</param>
        /// <param name="sentAt">Data de trimitere a mesajului</param>
        /// <param name="senderUsername">Numele utilizatorului care a trimis mesajul</param>
        public MessageDTO(string format, byte[] messageData, DateTime sentAt, string senderUsername)
        {
            Format = format;
            MessageData = messageData;
            SentAt = sentAt;
            SenderUsername = senderUsername;
        }
    }
}
