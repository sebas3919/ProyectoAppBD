using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBD.DAO
{
    public class UserListDAO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Rolename { get; set; }
    }
}
