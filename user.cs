using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text11_1
{
    class User
    {
        public User(string _Id,string _pass,bool _root,int[] _books)
        {
            Id = _Id;
            pass = _pass;
            root = _root;
            books = _books;
        }
        private string Id;
        private string pass;
        private bool root;
        private int[] books;
        
    }
}
