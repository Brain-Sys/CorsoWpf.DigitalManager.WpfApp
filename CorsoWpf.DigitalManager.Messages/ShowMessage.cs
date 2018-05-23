using System;
using System.Collections.Generic;
using System.Text;

namespace CorsoWpf.DigitalManager.Messages
{
    public class ShowMessage : BaseMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}