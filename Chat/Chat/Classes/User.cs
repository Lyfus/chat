using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Classes
{
    public class User
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        private List<Group> _groups = new List<Group>();
        public List<Group> Groups { get { return _groups; } }

        public User(int id, string pseudo, string password)
        {
            Id = id;
            Pseudo = pseudo;
            Password = password;
        }
    }
}
