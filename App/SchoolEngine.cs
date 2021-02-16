using System.Collections.Generic;
using CoreSchool.entities;

namespace CoreSchool
{
    public class SchoolEngine
    {
        public School School { get; set; }

        public SchoolEngine()
        {
            
        }

        public void Initialize()
        {
            // By setting optional parameters in the ctor, we can skip setting them, or just set one of them, 
            // or set them in a different order, in this case, country was supposed to be set before city.
            School = new School("Platzi Academy", 2012, SchoolTypes.HighSchool,
            city:"Bogota", country:"Colombia");

            // Course ctor does not ask for parameters to create an object, but we can give the parameters in between {}.
            // This list only uses Course-type objects
            School.courses = new List<Course>()
            {
                new Course() {name = "101", workShift = WorkShift.Morning},
                new Course() {name = "201", workShift = WorkShift.Morning},
                new Course() {name = "301", workShift = WorkShift.Morning},
                new Course() {name = "401", workShift = WorkShift.Evening},
                new Course() {name = "501", workShift = WorkShift.Evening},
            };

            /*
            =============== Ways to add more items to a generic list ========================
            =================================================================================

            School.courses.Add(new Course(){name = "102", workShift = WorkShift.Evening});

            var anotherCourseList = new List<Course>()
            {
                new Course() {name = "401", workShift = WorkShift.Morning},
                new Course() {name = "501", workShift = WorkShift.Morning},
                new Course() {name = "502", workShift = WorkShift.Evening}
            };

            // Adding a list to another list
            School.courses.AddRange(anotherCourseList);
            */

            /*
            =============== Ways to remove items from a generic list ========================
            =================================================================================
            
            Course tmp = new Course(){name = "101-Vacacional", workShift = WorkShift.Night};
            School.courses.Add(tmp);

            // The framework removes the object whose HashCode matches the HashCode of the parameter inside Remove().
            School.courses.Remove(tmp);

            // This structure can be used when there are more parameters to compare
            mySchool.courses.RemoveAll(delegate(Course courseToRemove)
            {return courseToRemove.name == "501";});

            // Shorter version using lambda expressions            
            mySchool.courses.RemoveAll((courseToRemove) => courseToRemove.name == "501");
            
            */
        }
    }
}