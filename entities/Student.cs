using System;

namespace CoreSchool.entities
{
    public class Student
    {
        public string name { get; set; }
        public string uniqueID { get; private set; }

        public Student() => uniqueID = Guid.NewGuid().ToString();
    }
}