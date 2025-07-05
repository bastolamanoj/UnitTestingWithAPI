using API.Controllers;
using API.Repository;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controller
{
    public class UserControllerTest
    {
        private readonly IUserRepository userRepository;
        private readonly UserController userController;
        public UserControllerTest()
        {
            //setup dependencies using FakeItEasy
            this.userRepository = A.Fake<IUserRepository>();

            //SUT (System Under Test)   
            this.userController = new UserController(userRepository);

        }


    }
}
