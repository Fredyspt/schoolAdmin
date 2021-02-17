using System;

namespace CoreSchool.entities
{
    public class Exam
    {
        public string name { get; set; }
        public string uniqueID { get; private set; }
        public Student student { get; set; }
        public Exam exam { get; set; }
        public float grade { get; set; }

        public Exam() => uniqueID = Guid.NewGuid().ToString();
    }
}