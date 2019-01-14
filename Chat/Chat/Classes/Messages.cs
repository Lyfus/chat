using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Chat.Classes
{
    public class Messages
    {
        public int IdMessage { get; set; }
        public DateTime PubDate { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public int IdGroup { get; set; }

        public Messages(int id, string pubDate, string content, string user, int idgroup)
        {
            IdMessage = id;
            PubDate = DateTime.Parse(pubDate);
            Content = content;
            Username = user;
            IdGroup = idgroup;
        }
    }
}
