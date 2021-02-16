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
    }
}