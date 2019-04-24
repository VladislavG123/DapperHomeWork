using System;

namespace DL.Models
{
    public class Mail : Entity
    {
        public string Theme { get; set; }
        public string Text { get; set; }

        public Guid ReceiverId { get; set; }
        public Receiver Receiver { get; set; }
    }
}