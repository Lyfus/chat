using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Classes
{
    class Group
    {
        public int Id { get; set; }
        public int Description { get; set; }
        private List<User> _users = new List<User>();
        public List<User> Users { get { return _users; } }

        public Group(int id, int description)
        {
            Id = id;
            Description = description;
        }
    }
}
