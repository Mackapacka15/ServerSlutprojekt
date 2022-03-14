using System;

namespace ServerSlutprojekt
{
    public class Person
    {
        
        static public List<Person> People = new List<Person>();
        public string Name { get; set; }
        public bool Here { get; set; }

        public Person()
        {
            People.Add(this);
        }
    }
}