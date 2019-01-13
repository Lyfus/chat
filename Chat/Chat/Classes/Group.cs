using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Classes
{
    public class Group
    {
        public int IdGroup { get; set; }
        public string Description { get; set; }
        private List<User> _users = new List<User>();
        public List<User> Users { get { return _users; } }

        public Group(int id, string description)
        {
            IdGroup = id;
            Description = description;
        }
    }
}
