using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Services.Services.User.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
