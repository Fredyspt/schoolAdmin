using System;
using System.Collections.Generic;

namespace CoreSchool.entities
{
    public class Student: BaseSchoolObject
    {
        public List<Exam> exams { get; set; }

        public Student(){
            exams = new List<Exam>();
        }
    }
}