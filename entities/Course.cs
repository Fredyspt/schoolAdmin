using System;
using System.Collections.Generic;

namespace CoreSchool.entities
{
    public class Course:BaseSchoolObject
    {
        // No one can set the uniqueID but the class
        public WorkShift workShift { get; set; }
        public List<Subject> subjects { get; set; }
        public List<Student> students { get; set; }
    }
}