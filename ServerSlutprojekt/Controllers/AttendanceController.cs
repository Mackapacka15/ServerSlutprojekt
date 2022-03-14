using System;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;


namespace ServerSlutprojekt.Controllers
{
    [Route("api/Attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {

        static AttendanceController()
        {
            string peopleString = System.IO.File.ReadAllText("People.json");
            Person.People = JsonSerializer.Deserialize<List<Person>>(peopleString);
        }

        [HttpGet]
        public ActionResult Get(string name)
        {
            bool personExists = Exists(name);

            if (personExists)
            {
                return Ok("Välkommen In");
            }
            else
            {
                return NotFound(name);
            }
        }

        [HttpPut]
        public ActionResult Put(string name)
        {


            if (!Exists(name))
            {
                string[] split = name.Split('─');
                new Person(split[0], split[1]);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool Exists(string name)
        {
            foreach (Person item in Person.People)
            {
                if ((item.Firstname + item.Lastname).ToLower() == name.ToLower())
                {
                    item.Here = true;
                    return true;
                }
            }
            return false;
        }
    }
}