using AutoAdoNet.Services.Services.User.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAdoNet.Console
{
    class ConsoleApplication
    {
        private readonly IUserService _userService;
        public ConsoleApplication(IUserService userService)
        { 
            _userService = userService;
        }

        public void Run()
        {
            _userService.Get();
        }

    }
}
