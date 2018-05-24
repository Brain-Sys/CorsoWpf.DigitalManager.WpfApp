using System;
using System.Collections.Generic;
using System.Text;

namespace CorsoWpf.DigitalManager.Messages
{
    public class SaveFileRequestMessage : BaseMessage
    {
        public string Title { get; set; }
        public string SuggestedFileName { get; set; }
        public bool Success { get; set; }
    }
}