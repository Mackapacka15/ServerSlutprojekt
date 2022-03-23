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
        static RandomList<int> newIdList = new RandomList<int>();
        static Random generator = new Random();

        static AttendanceController()
        {
            string peopleString = System.IO.File.ReadAllText("People.json");
            Person.people = JsonSerializer.Deserialize<List<Person>>(peopleString);
            PopulateId();
        }

        [HttpGet]
        public ActionResult Get(string name)
        {
            LoadData();
            if (name != null)
            {
                Console.WriteLine(name);

                string[] split = name.Split('─');
                if (split.Length == 2)
                {
                    bool personExists = Exists(split[0], split[1]);
                    if (personExists)
                    {
                        SaveData();
                        return Ok("Välkommen In");
                    }
                    else
                    {
                        return NotFound("F");
                    }
                }
            }
            SaveData();
            return Ok("F.2");
        }

        [HttpPost]
        public ActionResult Put(PersonIn person)
        {
            string name = person.name;
            Console.WriteLine("Name: " + name);
            if (name != null)
            {
                string[] split = name.Split('─');
                if (split.Length == 2)
                {
                    if (!Exists(split[0], split[1]))
                    {
                        new Person(split[0], split[1], newIdList.Get());
                        SaveData();
                        LoadData();
                        return Ok();
                    }
                }
            }
            return NotFound("Ha sämst");
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
        static void PopulateId()
        {
            List<int> usedId = new List<int>();
            int idAdd = 0;
            foreach (var item in Person.people)
            {
                usedId.Add(item.Id);
            }
            for (int i = 0; i < 200; i++)
            {
                idAdd = generator.Next(100000, 999999);
                if (!usedId.Contains(idAdd))
                {
                    newIdList.Add(idAdd);
                }
            }
        }
        static private void SaveData()
        {
            string jsonstring = JsonSerializer.Serialize<List<Person>>(Person.people);
            System.IO.File.WriteAllText("People.json", jsonstring);
        }
        static private void LoadData()
        {
            string peopleString = System.IO.File.ReadAllText("People.json");
            Person.people.Clear();
            Person.people = JsonSerializer.Deserialize<List<Person>>(peopleString);
        }
    }
}