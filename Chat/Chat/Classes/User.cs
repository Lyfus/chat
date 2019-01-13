using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Classes
{
    public class User
    {
        public int IdUser { get; set; }
        public string Pseudo { get; set; }
        public List<Group> Groups { get; set; }
        //public List<Group> Groups {
        //    get { return _groups; }
        //    set { _groups = value; }
        //}

        public User(int id, string pseudo)
        {
            IdUser = id;
            Pseudo = pseudo;           
        }
    }
}
