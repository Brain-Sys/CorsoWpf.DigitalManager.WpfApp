using System;
using System.Collections.Generic;
using System.Text;

namespace CorsoWpf.DigitalManager.Messages
{
    public class LoginSuccessfulMessage : BaseMessage
    {
        public DateTime LoginTimestamp { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
