using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Classes
{
    public class Messages
    {
        public int Id { get; set; }
        public DateTime PubDate { get; set; }
        public string Content { get; set; }
        public User User { get; set; }

        public Messages(int id, DateTime pubDate, string content, User user)
        {
            Id = id;
            PubDate = pubDate;
            Content = content;
            User = user;
        }
    }
}
