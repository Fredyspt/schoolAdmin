using System;

namespace CoreSchool.entities
{
    public class Exam:BaseSchoolObject
    {
        public double grade { get; set; }
        public Subject examSubject { get; set; }
        public Student testedStudent { get; set; }

        // We can override an existing method from the super class
        public override string ToString()
        {
            return $"{grade}, {testedStudent.name}, {examSubject.name}";
        }
    }
}