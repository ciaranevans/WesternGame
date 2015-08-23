using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GBWestern
{
    public class Message
    {
        private string id, text;

        public Message(string id, string text)
        {
            this.id = id;
            this.text = text;
        }

        public string Text
        {
            get
            {
                return text;
            }
        }
    }
}
