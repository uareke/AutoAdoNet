using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Models.User
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateBirth { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
