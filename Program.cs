using System;
using System.Collections.Generic;
using System.Linq;
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

            //engine.School.ClearData();

            // Search every obj of type IData, cast it into IData, and collect it.
            var interfaceList = from obj in objectList
                                where obj is IData
                                select (IData) obj;
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
