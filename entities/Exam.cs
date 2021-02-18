using System;

namespace CoreSchool.entities
{
    public class Exam:BaseSchoolObject
    {
        public double grade { get; set; }
        public Subject examSubject { get; set; }
        public Student testedStudent { get; set; }
    }
}