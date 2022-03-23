using System;

namespace ServerSlutprojekt
{
    public class Person
    {

        static public List<Person> people = new List<Person>();
        public string Firstname { get; init; }
        public string Lastname { get; init; }

        public bool Here { get; set; }
        public int Id { get; init; }

        public Person(string firstname, string lastname, int id)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            people.Add(this);
        }

    }
}