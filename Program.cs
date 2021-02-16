using System;
using System.Collections.Generic;
using CoreSchool.entities;
using static System.Console;

namespace Stage1
{
    class Program
    {
        static void Main(string[] args)
        {
            // By setting optional parameters in the ctor, we can skip setting them, or just set one of them, 
            // or set them in a different order, in this case, country was supposed to be set before city.

            var mySchool = new School("Platzi Academy", 2012, SchoolTypes.HighSchool,
            city:"Bogota", country:"Colombia");

            // Course ctor does not ask for parameters to create an object, but we can give the parameters in between {}.
            // This list only uses Course-type objects
            mySchool.courses = new List<Course>()
            {
                new Course() {name = "101", workShift = WorkShift.Morning},
                new Course() {name = "201", workShift = WorkShift.Morning},
                new Course() {name = "301", workShift = WorkShift.Morning}
            };

            mySchool.courses.Add(new Course(){name = "102", workShift = WorkShift.Evening});

            var anotherCourseList = new List<Course>()
            {
                new Course() {name = "401", workShift = WorkShift.Morning},
                new Course() {name = "501", workShift = WorkShift.Morning},
                new Course() {name = "502", workShift = WorkShift.Evening}
            };

            // Adding a list to another list
            mySchool.courses.AddRange(anotherCourseList);

            // Course tmp = new Course(){name = "101-Vacacional", workShift = WorkShift.Night};
            // mySchool.courses.Add(tmp);
            // The framework removes the object whose HashCode matches the HashCode of the parameter inside Remove().
            // mySchool.courses.Remove(tmp);

            print_school_courses(mySchool);

            // This structure can be used when there are more parameters to compare
            // mySchool.courses.RemoveAll(delegate(Course courseToRemove)
            // {return courseToRemove.name == "501";});

            // Shorter version using lambda expressions            
            mySchool.courses.RemoveAll((courseToRemove) => courseToRemove.name == "501");

            print_school_courses(mySchool);
        }

        public static void print_school_courses(School school)
        {
            WriteLine(new String('=', 20));
            WriteLine("School courses");
            WriteLine(new String('=', 20));

            // ? verifies the object (if it is != null) before verifying the attribute.
            if(school?.courses == null) return;
            foreach (var course in school.courses)
            {
                WriteLine($"Name: {course.name}, ID: {course.uniqueID}");
            }
        }
    }
}
