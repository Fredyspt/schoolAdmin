using System;
using CoreSchool.entities;

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
            
            var courseArray = new Course[3];

            // Course ctor does not ask for parameters to create an object, but we can give the parameters in between {}.
            courseArray[0] = new Course() {name = "101"};
            courseArray[1] = new Course() {name = "201"};
            courseArray[2] = new Course() {name = "301"};

            Console.WriteLine(mySchool);
            System.Console.WriteLine(new String('=', 20));
            Course.print_courses(courseArray);
        }
    }
}
