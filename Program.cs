using System;
using System.Collections.Generic;
using CoreSchool.entities;
using CoreSchool.Utilities;
using static System.Console;

namespace CoreSchool
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var engine =  new SchoolEngine();
            engine.Initialize();
        
            print_school_courses(engine.School);

            var objectList = engine.GetBaseSchoolObjects();
        }

        public static void print_school_courses(School school)
        {
            Printer.PrintTitle("School Courses");

            // ? verifies the object (if it is != null) before verifying the attribute.
            if(school?.courses == null) return;
            foreach (var course in school.courses)
            {
                WriteLine($"Name: {course.name}, ID: {course.uniqueID}");
            }
        }
    }
}
