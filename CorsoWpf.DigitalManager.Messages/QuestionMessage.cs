using System;
using System.Collections.Generic;
using System.Text;

namespace CorsoWpf.DigitalManager.Messages
{
    public class QuestionMessage : ShowMessage
    {
        public Action Yes { get; set; }
        public Action No { get; set; }
    }
}