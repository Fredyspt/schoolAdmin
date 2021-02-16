using System;
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

            mySchool.courses = new Course[]
            {
                new Course() {name = "101"},
                new Course() {name = "201"},
                new Course() {name = "301"}
            };

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
