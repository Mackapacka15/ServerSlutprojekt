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
        static Queueueueue<int> newIdList = new Queueueueue<int>();
        static List<string> keys = new List<string>();
        static Random generator = new Random();


        static AttendanceController()
        {
            string peopleString = System.IO.File.ReadAllText("People.json");
            Person.people = JsonSerializer.Deserialize<List<Person>>(peopleString);
            PopulateId();
        }

        [HttpGet]
        public ActionResult Get(PersonIn person)    
        {
            string name = person.Name;
            LoadData();
            if (name != null)
            {
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
                        return NotFound("Personen hittades inte");
                    }
                }
            }
            SaveData();
            return BadRequest("Felakting data Kunde inte läsas");
        }

        [HttpPost]
        public ActionResult Post(PersonIn person)
        {
            string name = person.Name;
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
                        return Ok("Tillagd i listan. Välkommen in");
                    }
                }
            }
            return BadRequest("Felaktig Data. Kunde inte läggas till");
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
        static private void LoadKeys()
        {
            List<KeyUser> keyUsers = new List<KeyUser>();
            string keyString = System.IO.File.ReadAllText("ApiKeys.json");
            keyUsers = JsonSerializer.Deserialize<List<KeyUser>>(keyString);
            foreach (var item in keyUsers)
            {
                keys.Add(item.Key);
            }
        }
    }
}