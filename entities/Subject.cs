using System;
using System.Collections.Generic;

namespace CoreSchool.entities
{
    public class Subject:BaseSchoolObject
    {
        public List<Exam> exams { get; set; }
        public Subject(){
            exams = new List<Exam>();
        } 
    }
}