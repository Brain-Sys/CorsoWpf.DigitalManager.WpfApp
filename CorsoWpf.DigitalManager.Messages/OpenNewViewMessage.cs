using System;
using System.Collections.Generic;
using System.Text;

namespace CorsoWpf.DigitalManager.Messages
{
    public class OpenNewViewMessage : BaseMessage
    {
        public string WindowName { get; }
        public bool Modal { get; set; }
        public object Parameter { get; set; }

        public OpenNewViewMessage(string name)
        {
            this.WindowName = name;
        }
    }

    public enum WindowsList
    {
        Login,
        MainMenu
    }
}