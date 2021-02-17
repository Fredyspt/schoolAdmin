using System;

namespace CoreSchool.entities
{
    public class Subject
    {
        public string name { get; set; }
        public string uniqueID { get; private set; }

        public Subject() => uniqueID = Guid.NewGuid().ToString();
    }
}