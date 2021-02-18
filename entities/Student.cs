using System;
using System.Collections.Generic;

namespace CoreSchool.entities
{
    public class Student
    {
        public string name { get; set; }
        public string uniqueID { get; private set; }

        public List<Exam> exams { get; set; }

        public Student(){
            uniqueID = Guid.NewGuid().ToString();
            exams = new List<Exam>();
        }
    }
}