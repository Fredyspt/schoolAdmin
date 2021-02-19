using System;
using System.Collections.Generic;
using CoreSchool.Utilities;

namespace CoreSchool.entities
{
    public class Course:BaseSchoolObject, IData
    {
        // No one can set the uniqueID but the class
        public WorkShift workShift { get; set; }
        public List<Subject> subjects { get; set; }
        public List<Student> students { get; set; }

        public void ClearData()
        {
            Printer.PrintTitle("Clearing course data...");
            Console.WriteLine($"Course {name} cleared");
        }
    }
}