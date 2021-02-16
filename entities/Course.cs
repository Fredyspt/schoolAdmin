using System;

namespace CoreSchool.entities
{
    public class Course
    {
        // No one can set the uniqueID but the class
        public string name { get; set; }
        public string uniqueID { get; private set; }

        public WorkShift workShift { get; set; }

        public Course() => uniqueID = Guid.NewGuid().ToString();

        // public override string ToString()
        // {
        //     return $"Name: {name}, ID: {uniqueID}";
        // }

        // A static function does not need an object to be used. It can be accessed by
        // Course.print_courses()

        // public static void print_courses(Course[] courseArray)
        // {
        //     foreach (var course in courseArray)
        //     {
        //         System.Console.WriteLine($"Name: {course.name}, ID: {course.uniqueID}");
        //     }
        // }
    }
}