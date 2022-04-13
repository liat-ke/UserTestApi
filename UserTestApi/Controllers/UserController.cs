using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserTestApi.Models;

namespace UserTestApi.Controllers
{
    [EnableCors()]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static List<User> UsersList = new List<User>()
        {
            new User
            {
                ID = 1,
                UserName = "LiatKe",
                FirstName = "Liat",
                LastName = "Danos",
                BirthDate = new DateTime(1998,04,30)
            },
               new User
            {
                ID = 2,
                UserName = "AdiKe",
                FirstName = "Alk",
                LastName = "Danos",
                BirthDate = new DateTime(1981,02,01)
            },
                  new User
            {
                ID = 3,
                UserName = "RotemKeshet",
                FirstName = "rk",
                LastName = "Keshet",
                BirthDate = new DateTime(1975,01,02)
            }
        };

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            User user = UsersList.Where(x => x.ID == id).FirstOrDefault();
            return Ok(user);
        }

        [HttpGet]
        public List<User> Get()
        {
            return UsersList;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User User)
        {
            if (id != User.ID)
                return BadRequest(new { message = "ID Does Not Exists" });

            User currentUser = UsersList.Where(x => x.ID == id).FirstOrDefault();
            if (!string.IsNullOrEmpty(User.FirstName) || !string.IsNullOrEmpty(User.LastName))
            {
                if (User.UserName.ToLower().Contains(User.FirstName.ToLower()) || User.UserName.ToLower().Contains(User.LastName.ToLower()))
                    return BadRequest("User Name Can Not Includes Your First Name Or Last Name");
            }
            currentUser.UserName = User.UserName;
            currentUser.LastName = User.LastName;
            currentUser.FirstName = User.FirstName;
            currentUser.BirthDate = User.BirthDate;
            return Ok(currentUser);
        }
    }
}
