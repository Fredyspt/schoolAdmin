using System;

namespace CoreSchool.entities
{
    public class Exam
    {
        public string name { get; set; }
        public string uniqueID { get; private set; }
        public double grade { get; set; }
        public Subject examSubject { get; set; }
        public Student testedStudent { get; set; }

        public Exam() => uniqueID = Guid.NewGuid().ToString();
    }
}