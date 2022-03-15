using System.Dynamic;
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
        static Random generator = new Random();

        static AttendanceController()
        {
            string peopleString = System.IO.File.ReadAllText("People.json");
            Person.people = JsonSerializer.Deserialize<List<Person>>(peopleString);
        }

        [HttpGet]
        public ActionResult Get(string name)
        {
            string[] split = name.Split('─');
            bool personExists = Exists(split[0], split[1]);

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
            string[] split = name.Split('─');
            if (!Exists(split[0], split[1]))
            {
                new Person(split[0], split[1], GenerateId());
                SaveData();
                LoadData();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool Exists(string firstname, string lastname)
        {
            foreach (Person item in Person.people)
            {
                if (item.Firstname.ToLower() == firstname.ToLower() && item.Lastname.ToLower() == lastname.ToLower())
                {
                    item.Here = true;
                    return true;
                }
            }
            return false;
        }
        static int GenerateId()
        {
            bool conflict = true;
            int id = 0;
            while (conflict)
            {
                conflict = false;
                id = generator.Next(100000, 999999);
                foreach (var item in Person.people)
                {
                    if (item.Id == id)
                    {
                        conflict = true;
                        break;
                    }
                }
            }
            return id;
        }
        private void SaveData()
        {
            string jsonstring = JsonSerializer.Serialize<List<Person>>(Person.people);
            System.IO.File.WriteAllText("People.json", jsonstring);
        }
        private void LoadData()
        {
            string peopleString = System.IO.File.ReadAllText("People.json");
            Person.people.Clear();
            Person.people = JsonSerializer.Deserialize<List<Person>>(peopleString);
        }
    }
}