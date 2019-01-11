using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Classes
{
    class Messages
    {
        public int Id { get; set; }
        DateTime PubDate { get; set; }
        string Content { get; set; }
        User User { get; set; }

        public Messages(int id, DateTime pubDate, string content, User user)
        {
            Id = id;
            PubDate = pubDate;
            Content = content;
            User = user;
        }
    }
}
