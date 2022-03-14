using System;

namespace ServerSlutprojekt
{
    public class Person
    {

        static public List<Person> People = new List<Person>();
        static Random generator = new Random();
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public bool Here { get; set; }
        public int Id { get; set; }

        public Person(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
            People.Add(this);
        }
        static int GenerateId()
        {
            bool conflict = true;
            int id = 0;
            while (conflict)
            {
                conflict = false;
                id = generator.Next(100000, 999999);
                foreach (var item in People)
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
    }
}