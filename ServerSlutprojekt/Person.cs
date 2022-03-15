using System;

namespace ServerSlutprojekt
{
    public class Person
    {

        static public List<Person> people = new List<Person>();
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public bool Here { get; set; }
        public int Id { get; set; }

        public Person(string firstname, string lastname, int id)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            people.Add(this);
        }

    }
}